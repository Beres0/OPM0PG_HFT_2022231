using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPM0PG_HFT_2022231.Logic;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;

namespace OPM0PG_HFT_2022231.Endpoint.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        IMusicLogic logic;
        public MusicController(IMusicLogic logic)
        {
            this.logic = logic;
        }
        [HttpGet("{albumId}")]
        public AlbumSummaryDTO GetAlbumSummary(int albumId)
        {
            return logic.GetAlbumSummary(albumId);
        }
        [HttpGet("{artistId}")]
        public ArtistSummaryDTO GetArtistSummary(int artistId)
        {
            return logic.GetArtistSummary(artistId);
        }
        [HttpGet]
        public StatisticsDTO GetStatistics()
        {
            return logic.GetStatistics();
        }
    }
}
