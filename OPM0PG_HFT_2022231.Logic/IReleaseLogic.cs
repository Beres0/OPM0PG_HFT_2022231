using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IReleaseLogic
    {
        void CreateRelease(Release release);

        IEnumerable<CountryStatDTO> GetCountryStatistics();

        IEnumerable<PublisherPerCountryDTO> GetPublisherPerCountry();

        IEnumerable<string> GetPublishers();

        IEnumerable<ReleasePerCountryDTO> GetReleasePerCountry();

        IEnumerable<ReleasePerYearDTO> GetReleasePerYear();

        IEnumerable<Release> ReadAllRelease();

        Release ReadRelease(int id);

        void UpdateRelease(Release release);
    }
}