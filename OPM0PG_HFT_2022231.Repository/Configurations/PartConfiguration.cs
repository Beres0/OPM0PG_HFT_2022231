using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class PartConfiguration : IEntityTypeConfiguration<Part>
    {
        public void Configure(EntityTypeBuilder<Part> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(p => p.Id);
            builder.HasOne(p => p.Album).WithMany(a => a.Parts).HasForeignKey(p => p.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetDefaultTextType(p => p.Title).IsRequired();
            builder.SetPositiveNumberType(nameof(Part.Position));
            builder.HasIndex(p => new { p.AlbumId, p.Position }).IsUnique();
            builder.SetSeed();
        }
    }
}