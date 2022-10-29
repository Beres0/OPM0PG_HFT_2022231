using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IMusicRepository
    {
        IRepository<Album> Albums { get; }
        IRepository<Artist> Artists { get; }
        IRepository<Contribution> Contributions { get; }
        IRepository<AlbumGenre> Genres { get; }
        IRepository<Membership> Memberships { get; }
        IRepository<Part> Parts { get; }
        IRepository<Release> Releases { get; }
        IRepository<Track> Tracks { get; }
    }
}