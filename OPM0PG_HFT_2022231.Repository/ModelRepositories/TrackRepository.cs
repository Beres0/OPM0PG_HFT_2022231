using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class TrackRepository : Repository<int, Track>
    {
        public TrackRepository(DbContext context) : base(context)
        {
        }
    }
}