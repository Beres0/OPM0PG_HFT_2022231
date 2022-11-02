using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IArtistLogic
    {
        void CreateMembership(Membership membership);

        void CreateArtist(Artist artist);

        void DeleteArtist(int id);

        IEnumerable<Artist> GetBands();

        IEnumerable<Artist> GetMembers(int bandId);

        IEnumerable<Artist> ReadAllArtist();

        IEnumerable<Membership> ReadAllMembership();

        Membership ReadMembership(int bandId, int memberId);

        Artist ReadArtist(int id);

        void DeleteMembership(int bandId, int memberId);

        void UpdateMembership(Membership membership);

        void UpdateArtist(Artist artist);
    }
}