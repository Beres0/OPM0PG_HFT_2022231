using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public class MusicRepository : IMusicRepository
    {
        private Dictionary<Type, object> repositories;

        public MusicRepository(MusicDbContext context)
        {
            repositories = new Dictionary<Type, object>();

            SetRepository(new Repository<Album>(context));
            SetRepository(new Repository<AlbumGenre>(context));
            SetRepository(new Repository<Artist>(context));
            SetRepository(new Repository<Contribution>(context));
            SetRepository(new Repository<Membership>(context));
            SetRepository(new Repository<Part>(context));
            SetRepository(new Repository<Release>(context));
            SetRepository(new Repository<Track>(context));
            SetRepository(new Repository<AlbumMedia>(context));
            SetRepository(new Repository<ArtistMedia>(context));
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

        private void SetRepository<TEntity>(IRepository<TEntity> repository)
                                                                            where TEntity : class, IEntity
        {
            repositories[typeof(TEntity)] = repository;
        }
    }
}