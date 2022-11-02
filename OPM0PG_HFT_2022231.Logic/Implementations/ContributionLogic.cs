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

        public void CreateContribution(Contribution contribution)
        {
            try
            {
                Validator<Contribution>.Validate(contribution.ArtistId);
                Validator<Contribution>.Validate(contribution.AlbumId);

                CheckKeyExists(repository.Artists, contribution.ArtistId);
                CheckKeyExists(repository.Albums, contribution.AlbumId);
                CheckKeyAlreadyAdded(repository.Contributions, nameof(contribution), contribution.GetId());
                repository.Contributions.Create(contribution);
            }
            catch (Exception ex)
            {
                throw new CreateException(contribution, ex);
            }
        }

        public IEnumerable<Contribution> ReadAllContributions()
        {
            return repository.Contributions.ReadAll();
        }

        public Contribution ReadContribution(int albumId, int artistId)
        {
            try
            {
                Validator<Contribution>.Validate(artistId);
                Validator<Contribution>.Validate(albumId);
                CheckKeyExists(repository.Contributions, "(artistId,albumId)", albumId, artistId);
                return repository.Contributions.Read(albumId, artistId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Contribution), ex, albumId, artistId);
            }
        }

        public void RemoveContribution(int albumId, int artistId)
        {
            try
            {
                Validator<Contribution>.Validate(artistId);
                Validator<Contribution>.Validate(albumId);
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