using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class PlaylistItemRepository : Repository<int, PlaylistItem>
    {
        public PlaylistItemRepository(DbContext context) : base(context)
        {
        }
    }
}