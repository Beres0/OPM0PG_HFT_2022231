using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.HasKey(p => new { p.AlbumId, p.Id });
            builder.HasOne(p => p.Album).WithMany(a => a.Parts).HasForeignKey(p => p.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetDefaultTextType(p => p.Title).IsRequired();
            builder.SetSeed();
        }
    }
}