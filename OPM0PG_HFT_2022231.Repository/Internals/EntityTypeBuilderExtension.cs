using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OPM0PG_HFT_2022231.Repository.Internals
{
    internal static class EntityTypeBuilderExtension
    {
        public static void SetSeed<TEntity>
            (this EntityTypeBuilder<TEntity> builder)
            where TEntity : class
        {
            builder.HasData(new XmlSerializer<List<TEntity>>().Deserialize($"Seeds/{typeof(TEntity).Name}Seed.xml"));
        }

        public static void SetDatabaseGeneratedPrimaryKey<TEntity>
            (this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            builder.HasKey(propertyExpression);
            builder.Property(propertyExpression).ValueGeneratedOnAdd();
        }

        public static PropertyBuilder SetPositiveNumberType<TEntity>
            (this EntityTypeBuilder<TEntity> builder, string propertyName)
            where TEntity : class
        {
            builder.HasCheckConstraint(propertyName, $"({propertyName}>0)");
            return builder.Property(propertyName);
        }

        public static PropertyBuilder<object> SetDefaultTextType<TEntity>
            (this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            return builder.Property(propertyExpression).HasColumnType("varchar").HasMaxLength(ColumnTypeConstants.MaxTextLength).IsUnicode(true);
        }

        public static PropertyBuilder SetDefaultYearType<TEntity>
            (this EntityTypeBuilder<TEntity> builder, string propertyName)
            where TEntity : class
        {
            builder.HasCheckConstraint(propertyName, $"({propertyName}>={ColumnTypeConstants.MinYear} and {propertyName}<={ColumnTypeConstants.MaxYear})");
            return builder.Property(propertyName);
        }
    }
}