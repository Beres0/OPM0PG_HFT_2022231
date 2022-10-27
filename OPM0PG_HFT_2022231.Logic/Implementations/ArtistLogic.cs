using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public class ArtistLogic : IArtistLogic
    {
        IRepository<int, Artist> Artists { get; }
        IRepository<object, Membership> Memberships { get; }


        public ArtistLogic(IRepository<int, Artist> artists,
                           IRepository<object, Membership> memberships)
        {
            Artists = artists;
            Memberships = memberships;
        }

        public void CreateArtist(Artist artist)
        {
            Artists.Create(artist);
        }
        public void AddMembership(int bandId, int memberId)
        {
            Memberships.Create(new Membership() { BandId = bandId, MemberId = memberId });
        }
        public Artist ReadArtist(int id)
        {
            return Artists.Read(id);
        }
        public IEnumerable<Artist> ReadAllArtist()
        {
            return Artists.ReadAll();
        }
        public IEnumerable<Membership> ReadAllMembership()
        {
            return Memberships.ReadAll();
        }
        public void UpdateArtist(Artist artist)
        {
            Artists.Update(artist);
        }
        public void SetMembershipStatus(int bandId, int memberId, bool active)
        {
            Memberships.Update(new Membership { BandId = bandId, MemberId = memberId, Active = active });
        }
        public void DeleteArtist(int id)
        {
            Artists.Delete(id);
        }
        public void RemoveMembership(int bandId, int memberId)
        {
            Memberships.Delete(new { bandId, memberId });
        }
        public IEnumerable<Artist> GetBands()
        {
            return Memberships.ReadAll()
                .Select(m => m.Band)
                .Distinct();
        }
        public IEnumerable<Artist> GetMembers(int bandId)
        {
            return Memberships.ReadAll()
                .Where(m => m.BandId == bandId)
                .Select(m => m.Member);
        }

    }
}
