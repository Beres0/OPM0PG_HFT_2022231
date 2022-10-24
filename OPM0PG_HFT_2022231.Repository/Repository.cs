using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Repository
{
    public class Repository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
    {
        private static readonly Action<TEntity, TEntity>[] updaters = CreateUpdaters();

        private static Action<TEntity, TEntity>[] CreateUpdaters()
        {
            Type type = typeof(TEntity);
            PropertyInfo[] props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty)
                .Where(p => !p.SetMethod.IsVirtual).ToArray();

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

        public void DeleteRange(IEnumerable<TKey> ids)
        {
            context.Set<TEntity>().RemoveRange(ReadRange(ids));
            context.SaveChanges();
        }

        public TEntity Read(TKey id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> ReadRange(IEnumerable<TKey> ids)
        {
            return ReadAll().Where((e) => ids.Contains(e.Id));
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
    }
}