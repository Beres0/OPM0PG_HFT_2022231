using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public class ContributorConfiguration : IEntityTypeConfiguration<Contributor>
    {
        public void Configure(EntityTypeBuilder<Contributor> builder)
        {
            builder.HasKey(c => new { c.AlbumId, c.ArtistId });
            builder.HasOne(c => c.Album).WithMany(a => a.Contributors).HasForeignKey(c => c.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(c => c.Artist).WithMany(a => a.ContributedAlbums).HasForeignKey(c => c.ArtistId).OnDelete(DeleteBehavior.ClientCascade); ;
            builder.SetSeed();
        }
    }
}