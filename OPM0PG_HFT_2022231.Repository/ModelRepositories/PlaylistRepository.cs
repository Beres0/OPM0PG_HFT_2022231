using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class PlaylistRepository : Repository<int, Playlist>
    {
        public PlaylistRepository(DbContext context) : base(context)
        {
        }
    }
}