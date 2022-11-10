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
            CreateEntity(() =>
            {
                artist.Id = 0;
                Validator<Artist>.Validate(artist.Name);
            }, artist);
        }

        public void CreateMembership(Membership membership)
        {
            CreateEntity(() =>
            {
                ValidateMembership(membership);
                CheckKeyAlreadyAdded<Membership>(membership.GetId());
            }, membership);
        }

        public void DeleteArtist(int artistId)
        {
            DeleteEntity<Artist>(() =>
            {
                Validator<Artist>.Validate(artistId, nameof(Artist.Id));
            }, artistId);
        }

        public void DeleteMembership(int bandId, int memberId)
        {
            DeleteEntity<Membership>(null, bandId, memberId);
        }

        public IEnumerable<Artist> GetBands()
        {
            return repository.ReadAll<Membership>()
                    .Select(m => m.Band)
                    .Distinct();
        }

        public IEnumerable<Artist> GetMembers(int bandId)
        {
            return QueryRead(() =>
            {
                Validator<Membership>.Validate(bandId);
                CheckKeyExists<Artist>(bandId);
                return repository.ReadAll<Membership>()
                      .Where(m => m.BandId == bandId)
                      .Select(m => m.Member);
            });
        }

        public IEnumerable<Artist> ReadAllArtist()
        {
            return repository.ReadAll<Artist>();
        }

        public IEnumerable<Membership> ReadAllMembership()
        {
            return repository.ReadAll<Membership>();
        }

        public Artist ReadArtist(int artistId)
        {
            return ReadEntityWithSimpleNumericKey<Artist>(artistId);
        }

        public Membership ReadMembership(int bandId, int memberId)
        {
            return ReadEntity<Membership>(null, bandId, memberId);
        }

        public void UpdateArtist(Artist artist)
        {
            UpdateEntity(() =>
            {
                Validator<Artist>.Validate(artist.Name);
            }, artist);
        }

        public void UpdateMembership(Membership membership)
        {
            UpdateEntity(null, membership);
        }

        private void ValidateMembership(Membership membership)
        {
            if (membership.BandId == membership.MemberId)
            {
                throw new ArgumentException($"'BandId' and 'MemberId' are the same!");
            }

            CheckKeyExists<Artist>(membership.MemberId);
            CheckKeyExists<Artist>(membership.BandId);

            if (repository.ReadAll<Membership>().Any(m => m.BandId == membership.MemberId))
            {
                throw new ArgumentException($"'The Membership.MemberId is already present as band in Memberships. To prevent circular references you can't add that id as member.");
            }
            if (repository.ReadAll<Membership>().Any(m => m.MemberId == membership.BandId))
            {
                throw new ArgumentException($"'The Membership.BandId' is already present as member in memberships. To prevent circular references you can't add that id as band.");
            }
        }
    }
}