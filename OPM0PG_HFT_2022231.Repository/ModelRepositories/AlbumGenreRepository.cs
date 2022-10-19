using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class AlbumGenreRepository : Repository<object, AlbumGenre>
    {
        public AlbumGenreRepository(DbContext context) : base(context)
        {
        }
    }
}