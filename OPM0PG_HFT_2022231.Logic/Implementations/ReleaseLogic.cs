using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Models.Support;
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

        public void CreateRelease(Release release)
        {
            if (release is null)
            {
                throw new ArgumentNullException(nameof(release));
            }

            try
            {
                release.Id = 0;
                ValidateRelease(release);
                repository.Releases.Create(release);
            }
            catch (Exception ex)
            {
                throw new CreateException(release, ex);
            }
        }

        public void DeleteRelease(int releaseId)
        {
            try
            {
                Validator<Release>.Validate(releaseId, nameof(Release.Id));
                CheckKeyExists(repository.Releases, releaseId);
                repository.Releases.Delete(releaseId);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Release), ex, releaseId);
            }
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
            return repository.Releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .Select(r => new { r.Country, r.Publisher })
                .Distinct()
                .GroupBy(r => r.Country)
                .Select(g => new PublisherPerCountryDTO(g.Key, g.Count()));
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

        public IEnumerable<ReleasePerYearDTO> GetReleasePerYear()
        {
            return repository.Releases.ReadAll().Where(r => r.ReleaseYear.HasValue)
                .GroupBy(r => r.ReleaseYear)
                .Select(g => new ReleasePerYearDTO(g.Key.Value, g.Count()));
        }

        public IEnumerable<Release> ReadAllRelease()
        {
            return repository.Releases.ReadAll();
        }

        public Release ReadRelease(int releaseId)
        {
            try
            {
                Validator<Release>.Validate(releaseId, nameof(Release.Id));
                CheckKeyExists(repository.Releases, releaseId);
                return repository.Releases.Read(releaseId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Release), ex, releaseId);
            }
        }

        public void UpdateRelease(Release release)
        {
            if (release is null)
            {
                throw new ArgumentNullException(nameof(release));
            }

            try
            {
                ValidateRelease(release);
                CheckKeyExists(repository.Releases, release.Id);
                repository.Releases.Update(release);
            }
            catch (Exception ex)
            {
                throw new UpdateException(release, ex);
            }
        }

        private void ValidateRelease(Release release)
        {
            Validator<Release>.Validate(release.Id);
            Validator<Release>.Validate(release.AlbumId);
            Validator<Release>.Validate(release.Country);
            Validator<Release>.Validate(release.Publisher);
            Validator<Release>.Validate(release.ReleaseYear);
            CheckKeyExists(repository.Albums, release.AlbumId);
        }
    }
}