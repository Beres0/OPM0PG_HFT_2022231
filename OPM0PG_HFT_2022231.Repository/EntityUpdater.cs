using OPM0PG_HFT_2022231.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Repository
{
    internal static class EntityUpdater<TEntity> where TEntity : class, IEntity
    {
        private static readonly Action<TEntity, TEntity>[] updaters = CreateUpdaters();

        private static Action<TEntity, TEntity>[] CreateUpdaters()
        {
            Type type = typeof(TEntity);
            PropertyInfo[] props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => !p.SetMethod.IsVirtual && p.CanWrite).ToArray();

            Action<TEntity, TEntity>[] updaters = new Action<TEntity, TEntity>[props.Length];

            var to = Expression.Parameter(type, "to");
            var from = Expression.Parameter(type, "from");

            for (int i = 0; i < props.Length; i++)
            {
                updaters[i] = Expression.Lambda<Action<TEntity, TEntity>>
                              (Expression.Assign(
                               Expression.Property(to, props[i].Name),
                               Expression.Property(from, props[i].Name)),
                               to, from)
                    .Compile();
            }

            return updaters;
        }

        public static void Update(TEntity to, TEntity from)
        {
            for (int i = 0; i < updaters.Length; i++)
            {
                updaters[i](to, from);
            }
        }
    }
}