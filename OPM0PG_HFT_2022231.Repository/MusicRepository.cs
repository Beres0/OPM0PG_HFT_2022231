using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class MusicRepository : IMusicRepository
    {
        public MusicRepository(MusicDbContext context)
        {
            Albums = new Repository<Album>(context);
            Genres = new Repository<AlbumGenre>(context);
            Artists = new Repository<Artist>(context);
            Contributions = new Repository<Contribution>(context);
            Memberships = new Repository<Membership>(context);
            Parts = new Repository<Part>(context);
            Releases = new Repository<Release>(context);
            Tracks = new Repository<Track>(context);
        }

        public IRepository<Album> Albums { get; }
        public IRepository<Artist> Artists { get; }
        public IRepository<Contribution> Contributions { get; }
        public IRepository<AlbumGenre> Genres { get; }
        public IRepository<Membership> Memberships { get; }
        public IRepository<Part> Parts { get; }
        public IRepository<Release> Releases { get; }
        public IRepository<Track> Tracks { get; }
    }
}