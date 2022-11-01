using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository;
using System;
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

        public void AddContribution(int albumId, int artistId)
        {
            Contribution contribution = new Contribution()
            {
                ArtistId = artistId,
                AlbumId = albumId
            };
            try
            {
                Validator<Contribution>.Throws(artistId);
                Validator<Contribution>.Throws(albumId);

                CheckKeyExists(repository.Artists, artistId);
                CheckKeyExists(repository.Albums, albumId);
                CheckKeyAlreadyAdded(repository.Contributions, "(artistId,albumId)", albumId, artistId);
                repository.Contributions.Create(contribution);
            }
            catch (Exception ex)
            {
                throw new CreateException(contribution, ex);
            }
        }

        public void RemoveContribution(int albumId, int artistId)
        {
            try
            {
                Validator<Contribution>.Throws(artistId);
                Validator<Contribution>.Throws(albumId);
                CheckKeyExists(repository.Contributions, "(artistId,albumId)", albumId, artistId);
                repository.Contributions.Delete(albumId, artistId);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Contribution), ex, albumId, artistId);
            }
        }
    }
}