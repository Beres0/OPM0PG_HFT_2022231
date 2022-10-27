using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(a => a.Id);
            builder.SetDefaultTextType(a => a.Title).IsRequired();
            builder.SetDefaultYearType(a => a.Year);
            builder.SetSeed();
        }
    }
}