using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ReleaseLogic : IReleaseLogic
    {
        IRepository<int,Release> releases;

        public ReleaseLogic(IRepository<int, Release> releases)
        {
            this.releases = releases;
        }

        public void CreateRelease(Release release)
        {
            releases.Create(release);
        }
        public Release ReadRelease(int id)
        {
            return releases.Read(id);
        }
        public IEnumerable<Release> ReadAllRelease()
        {
            return releases.ReadAll();
        }
        public void UpdateRelease(Release release)
        {
            releases.Update(release);
        }
        public IEnumerable<string> GetPublishers()
        {
            return releases.ReadAll()
                .Select(r => r.Publisher)
                .Distinct();
        }
        public IEnumerable<ReleasePerCountryDTO> GetReleasePerCountry()
        {
            return releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .GroupBy(r => r.Country)
                .Select(g => new ReleasePerCountryDTO(g.Key, g.Count()));
        }
        public IEnumerable<PublisherPerCountryDTO> GetPublisherPerCountry()
        {
            return releases.ReadAll().Where(r => !string.IsNullOrWhiteSpace(r.Country))
                .Select(r => new { r.Country, r.Publisher })
                .Distinct()
                .GroupBy(r => r.Country)
                .Select(g => new PublisherPerCountryDTO(g.Key, g.Count()));
        }
        public IEnumerable<ReleasePerYearDTO> GetReleasePerYear()
        {
            return releases.ReadAll().Where(r => r.ReleaseYear.HasValue)
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
