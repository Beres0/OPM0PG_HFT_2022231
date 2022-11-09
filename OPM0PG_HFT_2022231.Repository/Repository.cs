using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private RepositoryChainActions<TEntity> chainActions;
        private DbContext context;

        public Repository(MusicDbContext context)
        {
            this.context = context;
            chainActions = new RepositoryChainActions<TEntity>(context);
        }

        public IRepositoryChainActions<TEntity> ChainActions()
        {
            return chainActions;
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
            return context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> ReadAll()
        {
            return context.Set<TEntity>();
        }

        public bool TryRead(object[] id, out TEntity entity)
        {
            entity = context.Set<TEntity>().Find(id);
            return entity is not null;
        }

        public void Update(TEntity item)
        {
            TEntity source = Read(item.GetId());
            EntityCopier<TEntity>.Copy(source, item);
            context.SaveChanges();
        }
    }
}