using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => new { m.BandId, m.BandMemberId });
            builder.HasOne(m => m.Band).WithMany(a => a.Members).HasForeignKey(m => m.BandId).OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(m => m.BandMember).WithMany(a => a.Bands).HasForeignKey(m => m.BandMemberId).OnDelete(DeleteBehavior.ClientCascade);
            builder.SetSeed();
        }
    }
}