using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContributionController : ControllerBase,IContributionLogic
    {
        IContributionLogic logic;

        public ContributionController(IContributionLogic logic)
        {
            this.logic = logic;
        }
        [HttpPost("{artistId},{albumId}")]
        public void AddContribution(int artistId, int albumId)
        {
            logic.AddContribution(artistId, albumId);
        }
        [HttpGet]
        public IEnumerable<Contribution> ReadAllContributions()
        {
            return logic.ReadAllContributions();
        }
        [HttpDelete("{artistId}, {albumId}")]
        public void RemoveContribution(int artistId, int albumId)
        {
            logic.RemoveContribution(artistId, albumId);
        }
    }
}
