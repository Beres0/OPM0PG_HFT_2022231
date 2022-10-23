using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using System;

namespace OPM0PG_HFT_2022231.Repository
{
    public class MusicDbContext : DbContext
    {
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Contributor> Contributors { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Release> Releases { get; set; }
        public MusicDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Beres\\OneDrive\\OE-UMI\\repos\\3_felev\\Halfej\\OPM0PG_HFT_2022231\\OPM0PG_HFT_2022231.Repository\\Database\\MusicDb.mdf;Integrated Security=True;MultipleActiveResultSets=True";
            optionsBuilder.UseSqlServer(connStr);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}