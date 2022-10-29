using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(t => t.Id);

            builder.HasOne(t => t.Part).WithMany(p=>p.Tracks).HasForeignKey(t => t.PartId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetDefaultTextType(t => t.Title).IsRequired();
            builder.SetPositiveNumberType(nameof(Track.Position));
            builder.HasIndex(t => new { t.PartId, t.Position }).IsUnique();
            builder.SetSeed();
        }
    }
}