using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(t => new { t.AlbumId, t.PartId, t.Id });
            builder.HasOne(t => t.Album).WithMany(a => a.Tracks).HasForeignKey(t => t.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(t => t.Part).WithMany(p => p.Tracks).HasForeignKey(t => new { t.AlbumId, t.PartId }).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetDefaultTextType(t => t.Title).IsRequired();
            builder.SetSeed();
        }
    }
}