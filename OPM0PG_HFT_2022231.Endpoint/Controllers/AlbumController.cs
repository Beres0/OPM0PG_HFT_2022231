using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private IAlbumLogic logic;
        public AlbumController(IAlbumLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void CreateAlbum([FromBody] Album album)
        {
            logic.CreateAlbum(album);
        }
        [HttpPost]
        public void CreatePart([FromBody] Part part)
        {
            logic.CreatePart(part);
        }

        [HttpPost]
        public void CreateTrack([FromBody] Track track)
        {
            logic.CreateTrack(track);
        }
        [HttpDelete("{albumId}")]
        public void DeleteAlbum(int albumId)
        {
            logic.DeleteAlbum(albumId);
        }
        [HttpDelete("{partId}")]
        public void DeletePart(int partId)
        {
            logic.DeletePart(partId);
        }
        [HttpDelete("{albumId},{partPosition}")]
        public void DeletePartByPosition(int albumId, int partPosition)
        {
            logic.DeletePartByPosition(albumId, partPosition);
        }
        [HttpDelete("{trackId}")]
        public void DeleteTrack(int trackId)
        {
            logic.DeleteTrack(trackId);
        }
        [HttpDelete("{partId},{trackPosition}")]
        public void DeleteTrackByPosition(int partId, int trackPosition)
        {
            logic.DeleteTrackByPosition(partId, trackPosition);
        }
        [HttpDelete("{albumId},{partPosition},{trackPosition}")]
        public void DeleteTrackByPosition(int albumId, int partPosition, int trackPosition)
        {
            logic.DeleteTrackByPosition(albumId, partPosition, trackPosition);
        }
        [HttpGet]
        public IEnumerable<AlbumPerYearDTO> GetAlbumPerYear()
        {
            return logic.GetAlbumPerYear();
        }
        [HttpGet("{albumId}")]
        public TimeSpan GetTotalDurationOfAlbum(int albumId)
        {
            return logic.GetTotalDurationOfAlbum(albumId);
        }

        [HttpGet("{partId}")]
        public TimeSpan GetTotalDurationOfPart(int partId)
        {
            return logic.GetTotalDurationOfPart(partId);
        }
        [HttpGet("{albumId}")]
        public Album ReadAlbum(int albumId)
        {
            return logic.ReadAlbum(albumId);
        }
        [HttpGet]
        public IEnumerable<Album> ReadAllAlbum()
        {
            return logic.ReadAllAlbum();
        }
        [HttpGet]
        public IEnumerable<Part> ReadAllPart()
        {
            return logic.ReadAllPart();
        }

        [HttpGet]
        public IEnumerable<Track> ReadAllTrack()
        {
            return logic.ReadAllTrack();
        }
        [HttpGet("{partId}")]
        public Part ReadPart(int partId)
        {
            return logic.ReadPart(partId);
        }

        [HttpGet("{albumId},{partPosition}")]
        public Part ReadPartByPosition(int albumId, int partPosition)
        {
            return logic.ReadPartByPosition(albumId, partPosition);
        }
        [HttpGet("{trackId}")]
        public Track ReadTrack(int trackId)
        {
            return logic.ReadTrack(trackId);
        }
        [HttpGet("{partId},{trackPosition}")]
        public Track ReadTrackByPosition(int partId, int trackPosition)
        {
            return logic.ReadTrackByPosition(partId, trackPosition);
        }
        [HttpGet("{albumId},{partPosition},{trackPosition}")]
        public Track ReadTrackByPosition(int albumId, int partPosition, int trackPosition)
        {
            return logic.ReadTrackByPosition(albumId, partPosition, trackPosition);
        }
        [HttpPut]
        public void UpdateAlbum([FromBody]Album album)
        {
            logic.UpdateAlbum(album);
        }

        [HttpPut]
        public void UpdatePart([FromBody]Part part)
        {
            logic.UpdatePart(part);
        }

        [HttpPut]
        public void UpdateTrack([FromBody]Track track)
        {
            logic.UpdateTrack(track);
        }
    }
}
