using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistController : ControllerBase, IArtistLogic
    {
        private IArtistLogic logic;

        public ArtistController(IArtistLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("{bandId},{memberId}")]
        public void AddMembership(int bandId, int memberId)
        {
            logic.AddMembership(bandId, memberId);
        }

        [HttpPost]
        public void CreateArtist([FromBody] Artist artist)
        {
            logic.CreateArtist(artist);
        }

        [HttpDelete("{id}")]
        public void DeleteArtist(int id)
        {
            logic.DeleteArtist(id);
        }

        [HttpGet]
        public IEnumerable<Artist> GetBands()
        {
            return logic.GetBands();
        }

        [HttpGet("{bandId}")]
        public IEnumerable<Artist> GetMembers(int bandId)
        {
            return logic.GetMembers(bandId);
        }

        [HttpGet]
        public IEnumerable<Artist> ReadAllArtist()
        {
            return logic.ReadAllArtist();
        }

        [HttpGet]
        public IEnumerable<Membership> ReadAllMembership()
        {
            return logic.ReadAllMembership();
        }

        [HttpGet("{id}")]
        public Artist ReadArtist(int id)
        {
            return logic.ReadArtist(id);
        }

        [HttpDelete("{bandId},{memberId}")]
        public void RemoveMembership(int bandId, int memberId)
        {
            logic.RemoveMembership(bandId, memberId);
        }

        [HttpPut("{bandId},{memberId},{active}")]
        public void SetMembershipStatus(int bandId, int memberId, bool active)
        {
            logic.SetMembershipStatus(bandId, memberId, active);
        }

        [HttpPut]
        public void UpdateArtist([FromBody] Artist artist)
        {
            logic.UpdateArtist(artist);
        }
    }
}