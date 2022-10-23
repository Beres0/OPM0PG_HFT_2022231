using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.SetDefaultTextType(g => g.GenreType).IsRequired();
            builder.HasKey(g => new { g.GenreType, g.AlbumId });
            builder.HasOne(g => g.Album).WithMany(a => a.Genres).HasForeignKey(g => g.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetSeed();
        }
    }
}