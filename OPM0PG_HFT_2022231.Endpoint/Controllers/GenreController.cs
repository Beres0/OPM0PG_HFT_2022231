using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private IGenreLogic logic;

        public GenreController(IGenreLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost]
        public void CreateGenre([FromBody] AlbumGenre genre)
        {
            logic.CreateGenre(genre);
        }

        [HttpDelete("{albumId},{genre}")]
        public void DeleteGenre(int albumId, string genre)
        {
            logic.DeleteGenre(albumId, genre);
        }

        [HttpGet]
        public IEnumerable<AlbumPerGenreDTO> GetAlbumPerGenre()
        {
            return logic.GetAlbumPerGenre();
        }

        [HttpGet]
        public IEnumerable<ArtistPerGenreDTO> GetArtistPerGenre()
        {
            return logic.GetArtistPerGenre();
        }

        [HttpGet]
        public IEnumerable<string> GetGenres()
        {
            return logic.GetGenres();
        }

        [HttpGet]
        public IEnumerable<AlbumGenre> ReadAllAlbumGenre()
        {
            return logic.ReadAllAlbumGenre();
        }

        [HttpGet]
        public IEnumerable<ArtistGenreDTO> ReadAllArtistGenre()
        {
            return logic.ReadAllArtistGenre();
        }

        [HttpGet("{albumId},{genre}")]
        public AlbumGenre ReadGenre(int albumId, string genre)
        {
            return logic.ReadGenre(albumId, genre);
        }
    }
}