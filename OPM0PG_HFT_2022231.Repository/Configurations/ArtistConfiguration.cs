using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(a => a.Id);
            builder.SetDefaultTextType(a => a.Name).IsRequired();
            builder.SetSeed();
        }
    }
}