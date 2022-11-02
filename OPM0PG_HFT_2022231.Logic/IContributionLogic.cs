using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IContributionLogic
    {
        void CreateContribution(Contribution contribution);

        IEnumerable<Contribution> ReadAllContributions();

        Contribution ReadContribution(int albumId, int artistId);

        void RemoveContribution(int albumId, int artistId);
    }
}