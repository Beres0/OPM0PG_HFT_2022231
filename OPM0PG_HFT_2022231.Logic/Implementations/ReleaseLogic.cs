using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Utility;
using OPM0PG_HFT_2022231.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ReleaseLogic : BaseLogic, IReleaseLogic
    {
        public ReleaseLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        public void CreateRelease(Release release)
        {
            CreateEntity(() =>
            {
                release.Id = 0;
                ValidateRelease(release);
            }, release);
        }

        public void DeleteRelease(int releaseId)
        {
            DeleteEntityWithSimpleNumericKey<Release>(releaseId);
        }

        public IEnumerable<CountryStatDTO> GetCountryStatistics()
        {
            var publisherPerCountry = GetPublisherPerCountry();
            var releasePerCountry = GetReleasePerCountry();

            return publisherPerCountry.Join(releasePerCountry, (p) => p.Country, (r) => r.Country,
                (p, r) => new CountryStatDTO(p.Country, p.NumberOfPublishers, r.NumberOfReleases));
        }

        public IEnumerable<PublisherPerCountryDTO> GetPublisherPerCountry()
        {
            return ReadAllRelease().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .Select(r => new { r.Country, r.Publisher })
                .Distinct()
                .GroupBy(r => r.Country)
                .Select(g => new PublisherPerCountryDTO(g.Key, g.Count()));
        }

        public IEnumerable<string> GetPublishers()
        {
            return ReadAllRelease()
                .Select(r => r.Publisher)
                .Distinct();
        }

        public IEnumerable<ReleasePerCountryDTO> GetReleasePerCountry()
        {
            return ReadAllRelease().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .GroupBy(r => r.Country)
                .Select(g => new ReleasePerCountryDTO(g.Key, g.Count()));
        }

        public IEnumerable<ReleasePerYearDTO> GetReleasePerYear()
        {
            return ReadAllRelease().Where(r => r.ReleaseYear.HasValue)
                .GroupBy(r => r.ReleaseYear)
                .Select(g => new ReleasePerYearDTO(g.Key.Value, g.Count()));
        }

        public IEnumerable<Release> ReadAllRelease()
        {
            return repository.ReadAll<Release>();
        }

        public Release ReadRelease(int releaseId)
        {
            return ReadEntityWithSimpleNumericKey<Release>(releaseId);
        }

        public void UpdateRelease(Release release)
        {
            UpdateEntity(() =>
            {
                ValidateRelease(release);
            }, release);
        }

        private void ValidateRelease(Release release)
        {
            Validator<Release>.Validate(release.Country);
            Validator<Release>.Validate(release.Publisher);
            Validator<Release>.Validate(release.ReleaseYear);
            CheckKeyExists<Album>(release.AlbumId);
        }
    }
}