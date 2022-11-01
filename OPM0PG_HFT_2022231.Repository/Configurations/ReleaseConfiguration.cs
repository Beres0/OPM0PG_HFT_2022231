using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Configurations;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class ReleaseConfiguration : IEntityTypeConfiguration<Release>
    {
        public void Configure(EntityTypeBuilder<Release> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(r => r.Id);
            builder.HasOne(r => r.Album).WithMany(a => a.Releases).HasForeignKey(r => r.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetSeed();
        }
    }
}