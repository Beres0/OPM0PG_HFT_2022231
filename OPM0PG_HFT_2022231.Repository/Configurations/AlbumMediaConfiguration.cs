using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Configurations;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class AlbumMediaConfiguration : IEntityTypeConfiguration<AlbumMedia>
    {
        public void Configure(EntityTypeBuilder<AlbumMedia> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(m => m.Id);
            builder.HasOne(m => m.Album)
                   .WithMany(a => a.Media)
                   .HasForeignKey(m => m.AlbumId);
            builder.SetSeed();
        }
    }
}