using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Logic
{
    public interface IArtistLogic
    {
        void CreateArtist(Artist artist);

        void CreateMembership(Membership membership);

        void DeleteArtist(int id);

        void DeleteMembership(int bandId, int memberId);

        IEnumerable<Artist> GetBands();

        IEnumerable<Artist> GetMembers(int bandId);

        IEnumerable<Artist> ReadAllArtist();

        IEnumerable<Membership> ReadAllMembership();

        Artist ReadArtist(int id);

        Membership ReadMembership(int bandId, int memberId);

        void UpdateArtist(Artist artist);

        void UpdateMembership(Membership membership);
    }
}