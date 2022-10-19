using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Repository
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity : class, IEntity<TKey>
    {
        private DbContext context;

        public Repository(DbContext context)
        {
            this.context = context;
        }

        public void Create(TEntity item)
        {
            context.Set<TEntity>().Add(item);
            context.SaveChanges();
        }

        public void Delete(TKey id)
        {
            context.Set<TEntity>().Remove(Read(id));
            context.SaveChanges();
        }

        public TEntity Read(TKey id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> ReadAll()
        {
            return context.Set<TEntity>();
        }

        public void Update(TEntity item)
        {
            TEntity old = Read(item.Id);
            for (int i = 0; i < updaters.Length; i++)
            {
                updaters[i](item, old);
            }

            context.SaveChanges();
        }

        private static Action<TEntity, TEntity>[] updaters = CreateUpdaters();

        private static Action<TEntity, TEntity>[] CreateUpdaters()
        {
            Type type = typeof(TEntity);
            PropertyInfo[] props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanWrite && !p.SetMethod.IsVirtual).ToArray();

            Action<TEntity, TEntity>[] updaters = new Action<TEntity, TEntity>[props.Length];

            var from = Expression.Parameter(type, "from");
            var to = Expression.Parameter(type, "to");

            for (int i = 0; i < props.Length; i++)
            {
                updaters[i] = Expression.Lambda<Action<TEntity, TEntity>>(
                    Expression.Assign(
                        Expression.Property(to, props[i].Name), Expression.Property(from, props[i].Name))
                    , from, to).Compile();
            }
            return updaters;
        }
    }
}