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
                Validator<Artist>.Throws(artist.Id);
                Validator<Artist>.Throws(artist.Name);
                repository.Artists.Create(artist);
            }
            catch (Exception ex)
            {
                throw new CreateException(artist, ex);
            }
        }

        private void ValidateAddMembership(Membership membership)
        {
            if (membership.BandId == membership.MemberId)
            {
                throw new ArgumentException($"'BandId' and 'MemberId' are the same!");
            }
            Validator<Membership>.Throws(membership.BandId);
            Validator<Membership>.Throws(membership.MemberId);

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

        public void AddMembership(int bandId, int memberId)
        {
            Membership membership = new Membership()
            {
                BandId = bandId,
                MemberId = memberId,
                Active = true
            };
            try
            {
                ValidateAddMembership(membership);
                CheckKeyAlreadyAdded(repository.Memberships, $"(bandId,memberId)", bandId, memberId);
                repository.Memberships.Create(membership);
            }
            catch (Exception ex)
            {
                throw new CreateException(membership, ex);
            }
        }

        public Artist ReadArtist(int artistId)
        {
            try
            {
                Validator<Artist>.Throws(artistId, nameof(Artist.Id));
                return repository.Artists.Read(artistId);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Artist), ex, artistId);
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

        public void UpdateArtist(Artist artist)
        {
            if (artist is null)
            {
                throw new ArgumentNullException(nameof(artist));
            }

            try
            {
                Validator<Artist>.Throws(artist.Id);
                Validator<Artist>.Throws(artist.Name);
                CheckKeyExists(repository.Artists, artist.Id);
                repository.Artists.Update(artist);
            }
            catch (Exception ex)
            {
                throw new UpdateException(artist, ex);
            }
        }

        public void SetMembershipStatus(int bandId, int memberId, bool active)
        {
            Membership membership = new Membership()
            {
                BandId = bandId,
                MemberId = memberId,
                Active = active
            };
            try
            {
                Validator<Membership>.Throws(bandId);
                Validator<Membership>.Throws(memberId);
                CheckKeyExists(repository.Artists, bandId);
                CheckKeyExists(repository.Artists, memberId);
                CheckKeyExists(repository.Memberships, "(bandId,memberId)", bandId, memberId);
                repository.Memberships.Update(membership);
            }
            catch (Exception ex)
            {
                throw new UpdateException(membership, ex);
            }
        }

        public void DeleteArtist(int artistId)
        {
            try
            {
                Validator<Artist>.Throws(artistId, nameof(Artist.Id));
                CheckKeyExists(repository.Artists, artistId);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Artist), ex, artistId);
            }
        }

        public void RemoveMembership(int bandId, int memberId)
        {
            try
            {
                Validator<Membership>.Throws(bandId);
                Validator<Membership>.Throws(memberId);
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
                Validator<Membership>.Throws(bandId);
                return repository.Memberships.ReadAll()
                    .Where(m => m.BandId == bandId)
                    .Select(m => m.Member);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Artist), ex, bandId);
            }
        }
    }
}