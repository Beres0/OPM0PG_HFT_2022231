using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class AlbumGenreConfiguration : IEntityTypeConfiguration<AlbumGenre>
    {
        public void Configure(EntityTypeBuilder<AlbumGenre> builder)
        {
            builder.SetDefaultTextType(g => g.Genre).IsRequired();
            builder.HasKey(g => new { g.AlbumId, g.Genre });
            builder.HasOne(g => g.Album).WithMany(a => a.Genres).HasForeignKey(g => g.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetSeed();
        }
    }
}