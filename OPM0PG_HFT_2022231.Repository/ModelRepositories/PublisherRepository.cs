using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class PublisherRepository : Repository<int, Album>
    {
        public PublisherRepository(DbContext context) : base(context)
        {
        }
    }
}