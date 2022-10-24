﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OPM0PG_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace OPM0PG_HFT_2022231.Repository.Configuration
{
    public static class EntityTypeBuilderExtension
    {
        public static void SetSeed<TEntity>
            (this EntityTypeBuilder<TEntity> builder)
            where TEntity : class
        {
            builder.HasData(new XmlSerializer<List<TEntity>>().Deserialize($"Seed/{typeof(TEntity).Name}Seed.xml"));
        }

        public static void SetDatabaseGeneratedPrimaryKey<TEntity>
            (this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            builder.HasKey(propertyExpression);
            builder.Property(propertyExpression).ValueGeneratedOnAdd();
        }

        public static PropertyBuilder<object> SetDefaultTextType<TEntity>
            (this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            return builder.Property(propertyExpression).HasColumnType("varchar(255)");
        }

        public static PropertyBuilder<object> SetDefaultYearType<TEntity>
            (this EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object>> propertyExpression)
            where TEntity : class
        {
            return builder.Property(propertyExpression).HasPrecision(4);
        }
    }
}