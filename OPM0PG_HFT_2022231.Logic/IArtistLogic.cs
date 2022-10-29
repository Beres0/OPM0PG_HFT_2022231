using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IArtistLogic
    {
        void AddMembership(int bandId, int memberId);

        void CreateArtist(Artist artist);

        void DeleteArtist(int id);

        IEnumerable<Artist> GetBands();

        IEnumerable<Artist> GetMembers(int bandId);

        IEnumerable<Artist> ReadAllArtist();

        IEnumerable<Membership> ReadAllMembership();

        Artist ReadArtist(int id);

        void RemoveMembership(int bandId, int memberId);

        void SetMembershipStatus(int bandId, int memberId, bool active);

        void UpdateArtist(Artist artist);
    }
}