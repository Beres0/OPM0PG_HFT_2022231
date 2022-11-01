using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContributionController : ControllerBase, IContributionLogic
    {
        private IContributionLogic logic;

        public ContributionController(IContributionLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("{albumId},{artistId}")]
        public void AddContribution(int albumId, int artistId)
        {
            logic.AddContribution(albumId, artistId);
        }

        [HttpGet]
        public IEnumerable<Contribution> ReadAllContributions()
        {
            return logic.ReadAllContributions();
        }

        [HttpDelete("{albumId},{artistId}")]
        public void RemoveContribution(int albumId, int artistId)
        {
            logic.RemoveContribution(albumId, artistId);
        }
    }
}