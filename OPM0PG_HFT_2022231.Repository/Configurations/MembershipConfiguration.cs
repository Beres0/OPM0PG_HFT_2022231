using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Internals;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasKey(m => new { m.BandId, m.MemberId });
            builder.HasOne(m => m.Band).WithMany(a => a.Members).HasForeignKey(m => m.BandId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(m => m.Member).WithMany(a => a.Bands).HasForeignKey(m => m.MemberId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetSeed();
        }
    }
}