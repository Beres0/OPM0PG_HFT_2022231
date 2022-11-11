using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Utility.Reflection;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public class MusicRepository : IMusicRepository
    {
        private static readonly Dictionary<Type, Func<MusicDbContext, object>> Factories =
            GenericFactory.CreateFactoriesFromEntities<Func<MusicDbContext, object>>(typeof(Repository<>));

        private Dictionary<Type, object> repositories;

        public MusicRepository(MusicDbContext context)
        {
            repositories = new Dictionary<Type, object>();

            foreach (var factory in Factories)
            {
                repositories[factory.Key] = factory.Value(context);
            }
        }

        public void Create<TEntity>(TEntity entity)
           where TEntity : class, IEntity
        {
            GetRepository<TEntity>().Create(entity);
        }

        public void Delete<TEntity>(params object[] id)
           where TEntity : class, IEntity
        {
            GetRepository<TEntity>().Delete(id);
        }

        public TEntity Read<TEntity>(params object[] id)
           where TEntity : class, IEntity
        {
            return GetRepository<TEntity>().Read(id);
        }

        public IQueryable<TEntity> ReadAll<TEntity>()
           where TEntity : class, IEntity
        {
            return GetRepository<TEntity>().ReadAll();
        }

        public bool TryRead<TEntity>(object[] id, out TEntity entity)
           where TEntity : class, IEntity
        {
            return GetRepository<TEntity>().TryRead(id, out entity);
        }

        public void Update<TEntity>(TEntity entity)
           where TEntity : class, IEntity
        {
            GetRepository<TEntity>().Update(entity);
        }

        IRepositoryChainActions<TEntity> IMusicRepository.ChainActions<TEntity>()
        {
            return GetRepository<TEntity>().ChainActions();
        }

        private IRepository<TEntity> GetRepository<TEntity>()
           where TEntity : class, IEntity
        {
            return (IRepository<TEntity>)repositories[typeof(TEntity)];
        }
    }
}