using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public class ReleaseConfiguration : IEntityTypeConfiguration<Release>
    {
        public void Configure(EntityTypeBuilder<Release> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(r => r.Id);
            builder.HasOne(r => r.Album).WithMany(a => a.Releases).HasForeignKey(r => r.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetDefaultTextType(c => c.Country).IsRequired();
            builder.SetDefaultTextType((System.Linq.Expressions.Expression<System.Func<Release, object>>)(c => c.Publisher)).IsRequired();
            builder.SetDefaultYearType(c => c.ReleaseYear);
            builder.SetSeed();
        }
    }
}