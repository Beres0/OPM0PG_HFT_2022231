﻿using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private IArtistLogic logic;

        public ArtistController(IArtistLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void CreateArtist([FromBody] Artist artist)
        {
            logic.CreateArtist(artist);
        }

        [HttpPost]
        public void CreateMembership([FromBody] Membership membership)
        {
            logic.CreateMembership(membership);
        }

        [HttpDelete("{id}")]
        public void DeleteArtist(int id)
        {
            logic.DeleteArtist(id);
        }

        [HttpDelete("{bandId},{memberId}")]
        public void DeleteMembership(int bandId, int memberId)
        {
            logic.DeleteMembership(bandId, memberId);
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

        [HttpGet("{bandId},{memberId}")]
        public Membership ReadMembership(int bandId, int memberId)
        {
            return logic.ReadMembership(bandId, memberId);
        }

        [HttpPut]
        public void UpdateArtist([FromBody] Artist artist)
        {
            logic.UpdateArtist(artist);
        }

        [HttpPut]
        public void UpdateMembership([FromBody] Membership membership)
        {
            logic.UpdateMembership(membership);
        }
    }
}