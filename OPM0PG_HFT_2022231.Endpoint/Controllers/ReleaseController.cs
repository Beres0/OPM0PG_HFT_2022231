using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReleaseController : ControllerBase
    {
        IReleaseLogic logic;
        public ReleaseController(IReleaseLogic logic)
        {
            this.logic = logic;
        }
        [HttpPost]
        public void CreateRelease([FromBody]Release release)
        {
            logic.CreateRelease(release);
        }

        [HttpGet]
        public IEnumerable<CountryStatDTO> GetCountryStatistics()
        {
            return logic.GetCountryStatistics();
        }

        [HttpGet]
        public IEnumerable<PublisherPerCountryDTO> GetPublisherPerCountry()
        {
            return logic.GetPublisherPerCountry();
        }

        [HttpGet]
        public IEnumerable<string> GetPublishers()
        {
            return logic.GetPublishers();
        }

        [HttpGet]
        public IEnumerable<ReleasePerCountryDTO> GetReleasePerCountry()
        {
            return logic.GetReleasePerCountry();
        }

        [HttpGet]
        public IEnumerable<ReleasePerYearDTO> GetReleasePerYear()
        {
            return logic.GetReleasePerYear();
        }

        [HttpGet]
        public IEnumerable<Release> ReadAllRelease()
        {
            return logic.ReadAllRelease();
        }

        [HttpGet("{id}")]
        public Release ReadRelease(int id)
        {
            return logic.ReadRelease(id);
        }
        [HttpPut]
        public void UpdateRelease([FromBody]Release release)
        {
            logic.UpdateRelease(release);
        }
        [HttpDelete("{id}")]
        public void DeleteRelease(int id)
        {
            logic.DeleteRelease(id);
        }
    }
}
