using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class ContributorRepository : Repository<object, Contributor>
    {
        public ContributorRepository(DbContext context) : base(context)
        {
        }
    }
}