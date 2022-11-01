using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Configurations;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class AlbumGenreConfiguration : IEntityTypeConfiguration<AlbumGenre>
    {
        public void Configure(EntityTypeBuilder<AlbumGenre> builder)
        {
            builder.HasKey(g => new { g.AlbumId, g.Genre });
            builder.HasOne(g => g.Album).WithMany(a => a.Genres).HasForeignKey(g => g.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetSeed();
        }
    }
}