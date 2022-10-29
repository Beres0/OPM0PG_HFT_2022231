using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ContributionLogic : BaseLogic, IContributionLogic
    {
        public ContributionLogic(IMusicRepository musicRepository) : base(musicRepository)
        { }

        public IEnumerable<Contribution> ReadAllContributions()
        {
            return repository.Contributions.ReadAll();
        }

        public void AddContribution(int artistId, int albumId)
        {
            ValidatePositiveNumber(artistId);
            ValidatePositiveNumber(albumId);
            ValidateForeignKey(artistId, repository.Artists);
            ValidateForeignKey(albumId, repository.Albums);
            repository.Contributions.Create(new Contribution() { ArtistId = artistId, AlbumId = albumId });
        }

        public void RemoveContribution(int artistId, int albumId)
        {
            ValidatePositiveNumber(artistId);
            ValidatePositiveNumber(albumId);
            repository.Contributions.Delete(artistId, albumId);
        }
    }
}