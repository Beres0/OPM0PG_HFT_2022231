using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ContributionLogic : BaseLogic, IContributionLogic
    {
        public ContributionLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        public void CreateContribution(Contribution contribution)
        {
            CreateEntity(() =>
            {
                CheckKeyExists<Artist>(contribution.ArtistId);
                CheckKeyExists<Album>(contribution.AlbumId);
                CheckKeyAlreadyAdded<Contribution>(nameof(contribution), contribution.GetId());
            }, contribution);
        }

        public IEnumerable<Contribution> ReadAllContributions()
        {
            return repository.ReadAll<Contribution>();
        }

        public Contribution ReadContribution(int albumId, int artistId)
        {
            return ReadEntity<Contribution>(null, albumId, artistId);
        }

        public void RemoveContribution(int albumId, int artistId)
        {
            DeleteEntity<Contribution>(null, albumId, artistId);
        }
    }
}