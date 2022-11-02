using System;
using System.Linq.Expressions;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Support
{
    public static class EntityCopier<TEntity> where TEntity : class, IEntity
    {
        private static readonly Action<TEntity, TEntity>[] copiers = CreateCopiers();
        private static readonly Func<TEntity> factory = CreateFactory();

        public static void Copy(TEntity source, TEntity destination)
        {
            for (int i = 0; i < copiers.Length; i++)
            {
                copiers[i](source, destination);
            }
        }

        public static TEntity CopyToNew(TEntity entity)
        {
            TEntity copy = factory();
            Copy(entity, copy);
            return copy;
        }

        private static Action<TEntity, TEntity>[] CreateCopiers()
        {
            Type type = typeof(TEntity);
            PropertyInfo[] props = EntityPropertyCollector<TEntity>.CollectProperties();

            Action<TEntity, TEntity>[] updaters = new Action<TEntity, TEntity>[props.Length];

            var source = Expression.Parameter(type, "source");
            var destination = Expression.Parameter(type, "destination");

            for (int i = 0; i < props.Length; i++)
            {
                updaters[i] = Expression.Lambda<Action<TEntity, TEntity>>
                              (Expression.Assign(
                               Expression.Property(destination, props[i].Name),
                               Expression.Property(source, props[i].Name)),
                               source, destination)
                    .Compile();
            }

            return updaters;
        }

        private static Func<TEntity> CreateFactory()
        {
            return Expression.Lambda<Func<TEntity>>(Expression.New(typeof(TEntity).GetConstructor(new Type[] { }))).Compile();
        }
    }
}