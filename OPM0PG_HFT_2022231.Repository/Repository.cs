using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.Internals;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private DbContext context;

        private RepositoryChainActions<TEntity> chainActions;

        public Repository(MusicDbContext context)
        {
            this.context = context;
            chainActions = new RepositoryChainActions<TEntity>(context);
        }

        public void Create(TEntity item)
        {
            context.Set<TEntity>().Add(item);
            context.SaveChanges();
        }

        public void Delete(params object[] id)
        {
            context.Set<TEntity>().Remove(Read(id));
            context.SaveChanges();
        }

        public TEntity Read(params object[] id)
        {
            return context.Set<TEntity>().Find(id) is TEntity entity ?
            entity : throw new KeyNotFoundException($"The given ({string.Join(", ", id)}) id not found in '{typeof(TEntity).Name}' repository!");
        }

        public IEnumerable<TEntity> ReadAll()
        {
            return context.Set<TEntity>();
        }

        public void Update(TEntity item)
        {
            TEntity old = Read(item.GetId());
            EntityUpdater<TEntity>.Update(old, item);
            context.SaveChanges();
        }

        public IRepositoryChainActions<TEntity> ChainActions()
        {
            return chainActions;
        }
    }
}