using OPM0PG_HFT_2022231.Logic.Internals;
using OPM0PG_HFT_2022231.Models;
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

        private void ValidateArtist(Artist artist)
        {
            if (artist is null)
            {
                throw new ArgumentNullException(nameof(artist));
            }
            Validator.ValidateRequiredText(artist.Name);
        }

        private void ValidateMembershipKeys(int bandId, int memberId)
        {
            if (bandId == memberId)
            {
                throw new ArgumentException($"'bandId' and 'memberId' are the same!");
            }
            Validator.ValidatePositiveNumber(bandId);
            Validator.ValidatePositiveNumber(memberId);
            Validator.ValidateForeignKey(memberId, repository.Artists);
            Validator.ValidateForeignKey(bandId, repository.Artists);
            if (repository.Memberships.ReadAll().Any(m => m.BandId == memberId))
            {
                throw new InvalidOperationException($"'The ({memberId}) 'memberId' is already present as band in memberships. To prevent circular references you can't add that id as member.");
            }
            if (repository.Memberships.ReadAll().Any(m => m.MemberId == bandId))
            {
                throw new InvalidOperationException($"'The ({bandId}) 'bandId' is already present as member in memberships. To prevent circular references you can't add that id as band.");
            }
        }

        public void CreateArtist(Artist artist)
        {
            artist.Id = 0;
            ValidateArtist(artist);
            repository.Artists.Create(artist);
        }

        public void AddMembership(int bandId, int memberId)
        {
            ValidateMembershipKeys(bandId, memberId);
            repository.Memberships.Create(new Membership() { BandId = bandId, MemberId = memberId,Active=true });
        }

        public Artist ReadArtist(int artistId)
        {
            Validator.ValidatePositiveNumber(artistId);
            return repository.Artists.Read(artistId);
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
            Validator.ValidatePositiveNumber(artist.Id);
            ValidateArtist(artist);
            repository.Artists.Update(artist);
        }

        public void SetMembershipStatus(int bandId, int memberId, bool active)
        {
            ValidateMembershipKeys(bandId, memberId);
            repository.Memberships.Update(new Membership { BandId = bandId, MemberId = memberId, Active = active });
        }

        public void DeleteArtist(int artistId)
        {
            Validator.ValidatePositiveNumber(artistId);
            repository.Artists.Delete(artistId);
        }

        public void RemoveMembership(int bandId, int memberId)
        {
            ValidateMembershipKeys(bandId, memberId);
            repository.Memberships.Delete(bandId, memberId);
        }

        public IEnumerable<Artist> GetBands()
        {
            return repository.Memberships.ReadAll()
                .Select(m => m.Band)
                .Distinct();
        }

        public IEnumerable<Artist> GetMembers(int bandId)
        {
            Validator.ValidatePositiveNumber(bandId);
            return repository.Memberships.ReadAll()
                .Where(m => m.BandId == bandId)
                .Select(m => m.Member);
        }
    }
}