using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class AlbumRepository : Repository<int, Album>
    {
        public AlbumRepository(DbContext context) : base(context)
        {
        }
    }
}