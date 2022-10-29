using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IContributionLogic
    {
        void AddContribution(int artistId, int albumId);

        IEnumerable<Contribution> ReadAllContributions();

        void RemoveContribution(int artistId, int albumId);
    }
}