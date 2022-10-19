using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class UserRepository : Repository<int, User>
    {
        public UserRepository(DbContext context) : base(context)
        {
        }
    }
}