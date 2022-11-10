﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Configurations;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    internal class ArtistMediaConfiguration : IEntityTypeConfiguration<ArtistMedia>
    {
        public void Configure(EntityTypeBuilder<ArtistMedia> builder)
        {
            builder.SetDatabaseGeneratedPrimaryKey(m => m.Id);
            builder.HasOne(m => m.Artist)
                   .WithMany(a => a.Media)
                   .HasForeignKey(m => m.ArtistId);
            builder.SetSeed();
        }
    }
}