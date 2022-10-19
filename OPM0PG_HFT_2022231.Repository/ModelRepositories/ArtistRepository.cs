using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class ArtistRepository : Repository<int, Artist>
    {
        public ArtistRepository(DbContext context) : base(context)
        {
        }
    }
}