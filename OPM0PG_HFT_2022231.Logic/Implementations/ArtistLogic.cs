using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ArtistLogic : BaseLogic, IArtistLogic
    {
        public ArtistLogic(IMusicRepository musicRepository)
        : base(musicRepository)
        { }

        public void CreateArtist(Artist artist)
        {
            if (artist is null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            try
            {
                artist.Id = 0;
                Validator<Artist>.Validate(artist.Id);
                Validator<Artist>.Validate(artist.Name);
                repository.Artists.Create(artist);
            }
            catch (Exception ex)
            {
                throw new CreateException(artist, ex);
            }
        }

        public void CreateMembership(Membership membership)
        {
            if (membership is null)
            {
                throw new ArgumentNullException(nameof(membership));
            }

            try
            {
                ValidateMembership(membership);
                CheckKeyAlreadyAdded(repository.Memberships, nameof(membership), membership.GetId());
                repository.Memberships.Create(membership);
            }
            catch (Exception ex)
            {
                throw new CreateException(membership, ex);
            }
        }

        public void DeleteArtist(int artistId)
        {
            try
            {
                Validator<Artist>.Validate(artistId, nameof(Artist.Id));
                CheckKeyExists(repository.Artists, artistId);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Artist), ex, artistId);
            }
        }

        public void DeleteMembership(int bandId, int memberId)
        {
            try
            {
                Validator<Membership>.Validate(bandId);
                Validator<Membership>.Validate(memberId);
                CheckKeyExists(repository.Memberships, "(bandId,memberId)", bandId, memberId);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Membership), ex, bandId, memberId);
            }
        }

        public IEnumerable<Artist> GetBands()
        {
            return repository.Memberships.ReadAll()
                .Select(m => m.Band)
                .Distinct();
        }

        public IEnumerable<Artist> GetMembers(int bandId)
        {
            try
            {
                Validator<Membership>.Validate(bandId);
                CheckKeyExists(repository.Artists, bandId);
                return repository.Memberships.ReadAll()
                    .Where(m => m.BandId == bandId)
                    .Select(m => m.Member);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Artist), ex, bandId);
            }
        }

        public IEnumerable<Artist> ReadAllArtist()
        {
            return repository.Artists.ReadAll();
        }

        public IEnumerable<Membership> ReadAllMembership()
        {
            return repository.Memberships.ReadAll();
        }

        public Artist ReadArtist(int artistId)
        {
            try
            {
                Validator<Artist>.Validate(artistId, nameof(Artist.Id));
                CheckKeyExists(repository.Artists, artistId);
                return repository.Artists.Read(artistId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Artist), ex, artistId);
            }
        }

        public Membership ReadMembership(int bandId, int memberId)
        {
            try
            {
                Validator<Membership>.Validate(bandId);
                Validator<Membership>.Validate(memberId);
                CheckKeyExists(repository.Memberships, "(bandId,memberId)", bandId, memberId);
                return repository.Memberships.Read(bandId, memberId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Membership), ex, bandId, memberId);
            }
        }

        public void UpdateArtist(Artist artist)
        {
            if (artist is null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            try
            {
                Validator<Artist>.Validate(artist.Id);
                Validator<Artist>.Validate(artist.Name);
                CheckKeyExists(repository.Artists, artist.Id);
                repository.Artists.Update(artist);
            }
            catch (Exception ex)
            {
                throw new UpdateException(artist, ex);
            }
        }

        public void UpdateMembership(Membership membership)
        {
            if (membership is null)
            {
                throw new ArgumentNullException(nameof(membership));
            }

            try
            {
                Validator<Membership>.Validate(membership.BandId);
                Validator<Membership>.Validate(membership.MemberId);
                CheckKeyExists(repository.Artists, membership.BandId);
                CheckKeyExists(repository.Artists, membership.MemberId);
                CheckKeyExists(repository.Memberships, nameof(Membership), membership.GetId());
                repository.Memberships.Update(membership);
            }
            catch (Exception ex)
            {
                throw new UpdateException(membership, ex);
            }
        }

        private void ValidateMembership(Membership membership)
        {
            if (membership.BandId == membership.MemberId)
            {
                throw new ArgumentException($"'BandId' and 'MemberId' are the same!");
            }
            Validator<Membership>.Validate(membership.BandId);
            Validator<Membership>.Validate(membership.MemberId);

            CheckKeyExists(repository.Artists, membership.MemberId);
            CheckKeyExists(repository.Artists, membership.BandId);

            if (repository.Memberships.ReadAll().Any(m => m.BandId == membership.MemberId))
            {
                throw new ArgumentException($"'The Membership.MemberId is already present as band in Memberships. To prevent circular references you can't add that id as member.");
            }
            if (repository.Memberships.ReadAll().Any(m => m.MemberId == membership.BandId))
            {
                throw new ArgumentException($"'The Membership.BandId' is already present as member in memberships. To prevent circular references you can't add that id as band.");
            }
        }
    }
}