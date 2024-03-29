﻿using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Configuration;

namespace OPM0PG_HFT_2022231.Repository
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<AlbumGenre> AlbumGenres { get; set; }
        public DbSet<AlbumMedia> AlbumMedia { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<AlbumMedia> ArtistMedia { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseInMemoryDatabase("MusicDb");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumMediaConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistMediaConfiguration());
            modelBuilder.ApplyConfiguration(new ArtistConfiguration());
            modelBuilder.ApplyConfiguration(new ContributionConfiguration());
            modelBuilder.ApplyConfiguration(new AlbumGenreConfiguration());
            modelBuilder.ApplyConfiguration(new MembershipConfiguration());
            modelBuilder.ApplyConfiguration(new PartConfiguration());
            modelBuilder.ApplyConfiguration(new ReleaseConfiguration());
            modelBuilder.ApplyConfiguration(new TrackConfiguration());
        }
    }
}