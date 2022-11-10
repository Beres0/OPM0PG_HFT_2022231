using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    public class MediaController
    {
        private IMediaLogic logic;

        public MediaController(IMediaLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void CreateAlbumMedia([FromBody] AlbumMedia albumMedia)
        {
            logic.CreateAlbumMedia(albumMedia);
        }

        [HttpPost]
        public void CreateArtistMedia([FromBody] ArtistMedia artistMedia)
        {
            logic.CreateArtistMedia(artistMedia);
        }

        [HttpDelete]
        public void DeleteAlbumMedia(int albumMediaId)
        {
            logic.DeleteAlbumMedia(albumMediaId);
        }

        [HttpDelete]
        public void DeleteArtistMedia(int artistMediaId)
        {
            logic.DeleteArtistMedia(artistMediaId);
        }

        [HttpGet]
        public AlbumMedia ReadAlbumMedia(int albumMediaId)
        {
            return logic.ReadAlbumMedia(albumMediaId);
        }

        [HttpGet]
        public IEnumerable<AlbumMedia> ReadAllAlbumMedia()
        {
            return logic.ReadAllAlbumMedia();
        }

        [HttpGet]
        public IEnumerable<ArtistMedia> ReadAllArtistMedia()
        {
            return logic.ReadAllArtistMedia();
        }

        [HttpGet]
        public ArtistMedia ReadArtistMedia(int artistMediaId)
        {
            return logic.ReadArtistMedia(artistMediaId);
        }

        [HttpPut]
        public void UpdateAlbumMedia([FromBody] AlbumMedia albumMedia)
        {
            logic.UpdateAlbumMedia(albumMedia);
        }

        [HttpPut]
        public void UpdateArtistMedia([FromBody] ArtistMedia artistMedia)
        {
            logic.UpdateArtistMedia(artistMedia);
        }
    }
}