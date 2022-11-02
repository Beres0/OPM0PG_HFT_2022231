using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Repository.Xml;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OPM0PG_HFT_2022231.Repository.Configurations
{
    internal static class EntityTypeBuilderExtension
    {
        public static void SetDatabaseGeneratedPrimaryKey<TEntity>
            (this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            builder.HasKey(propertyExpression);
            builder.Property(propertyExpression).ValueGeneratedOnAdd();
        }

        public static void SetSeed<TEntity>
                    (this EntityTypeBuilder<TEntity> builder)
            where TEntity : class
        {
            builder.HasData(new XmlSerializer<List<TEntity>>().Deserialize($"Seeds/{typeof(TEntity).Name}Seed.xml"));
        }
    }
}