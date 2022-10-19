using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class LikeRepository : Repository<object, Like>
    {
        public LikeRepository(DbContext context) : base(context)
        {
        }
    }
}