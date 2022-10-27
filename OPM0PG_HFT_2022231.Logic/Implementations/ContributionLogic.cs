using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ContributionLogic : IContributionLogic
    {
        IRepository<Contribution> contributions;

        public ContributionLogic(IRepository<Contribution> contributions)
        {
            this.contributions = contributions;
        }

        public IEnumerable<Contribution> ReadAllContributions()
        {
            return contributions.ReadAll();
        }
        public void AddContribution(int artistId, int albumId)
        {
            contributions.Create(new Contribution() { ArtistId = artistId, AlbumId = albumId });
        }
        public void RemoveContribution(int artistId, int albumId)
        {
            contributions.Delete(artistId, albumId);
        }
    }
}
