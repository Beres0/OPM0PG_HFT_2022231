using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Configurations;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(a => a.Id);
            builder.SetSeed();
        }
    }
}