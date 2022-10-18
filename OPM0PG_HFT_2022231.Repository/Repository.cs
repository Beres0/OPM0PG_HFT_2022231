using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM0PG_HFT_2022231.Repository
{
    internal class Repository<TKey, TEntity> : IRepository<TKey, TEntity>
        where TEntity :class, IEntity<TKey>
    {
        DbContext context;
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
            context.Remove(Read(id));
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
            Read(item.Id);
            context.SaveChanges();
        }
    }
}
