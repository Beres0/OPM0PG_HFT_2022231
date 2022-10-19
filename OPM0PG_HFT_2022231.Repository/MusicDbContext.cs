using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumGenre> AlbumGenres { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public Publisher Publishers { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }

        public MusicDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer("");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}