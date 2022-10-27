using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class ContributionConfiguration : IEntityTypeConfiguration<Contribution>
    {
        public void Configure(EntityTypeBuilder<Contribution> builder)
        {
            builder.HasKey(c => new { c.AlbumId, c.ArtistId });
            builder.HasOne(c => c.Album).WithMany(a => a.Contributions).HasForeignKey(c => c.AlbumId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(c => c.Artist).WithMany(a => a.ContributedAlbums).HasForeignKey(c => c.ArtistId).OnDelete(DeleteBehavior.ClientCascade); ;
            builder.SetSeed();
        }
    }
}