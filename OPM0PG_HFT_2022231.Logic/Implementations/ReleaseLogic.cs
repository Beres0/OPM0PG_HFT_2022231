using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ReleaseLogic : BaseLogic, IReleaseLogic
    {
        public ReleaseLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        private void ValidateRelease(Release release)
        {
            if (release is null)
            {
                throw new ArgumentNullException(nameof(release));
            }

            ValidateText(release.Country);
            ValidateText(release.Publisher);
            ValidateYear(release.ReleaseYear);
            ValidateForeignKey(release.AlbumId, repository.Albums);
        }

        public void CreateRelease(Release release)
        {
            release.Id = 0;
            ValidateRelease(release);
            repository.Releases.Create(release);
        }

        public Release ReadRelease(int id)
        {
            ValidatePositiveNumber(id);
            return repository.Releases.Read(id);
        }

        public IEnumerable<Release> ReadAllRelease()
        {
            return repository.Releases.ReadAll();
        }

        public void UpdateRelease(Release release)
        {
            ValidateRelease(release);
            ValidatePositiveNumber(release.Id);
            repository.Releases.Update(release);
        }

        public IEnumerable<string> GetPublishers()
        {
            return repository.Releases.ReadAll()
                .Select(r => r.Publisher)
                .Distinct();
        }

        public IEnumerable<ReleasePerCountryDTO> GetReleasePerCountry()
        {
            return repository.Releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .GroupBy(r => r.Country)
                .Select(g => new ReleasePerCountryDTO(g.Key, g.Count()));
        }

        public IEnumerable<PublisherPerCountryDTO> GetPublisherPerCountry()
        {
            return repository.Releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .Select(r => new { r.Country, r.Publisher })
                .Distinct()
                .GroupBy(r => r.Country)
                .Select(g => new PublisherPerCountryDTO(g.Key, g.Count()));
        }

        public IEnumerable<ReleasePerYearDTO> GetReleasePerYear()
        {
            return repository.Releases.ReadAll().Where(r => r.ReleaseYear.HasValue)
                .GroupBy(r => r.ReleaseYear)
                .Select(g => new ReleasePerYearDTO(g.Key.Value, g.Count()));
        }

        public IEnumerable<CountryStatDTO> GetCountryStatistics()
        {
            var publisherPerCountry = GetPublisherPerCountry();
            var releasePerCountry = GetReleasePerCountry();

            return publisherPerCountry.Join(releasePerCountry, (p) => p.Country, (r) => r.Country,
                (p, r) => new CountryStatDTO(p.Country, p.NumberOfPublishers, r.NumberOfReleases));
        }
    }
}