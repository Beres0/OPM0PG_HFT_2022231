using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IMusicRepository
    {
        IRepositoryChainActions<TEntity> ChainActions<TEntity>() where TEntity : class, IEntity;

        void Create<TEntity>(TEntity entity) where TEntity : class, IEntity;

        void Delete<TEntity>(params object[] id) where TEntity : class, IEntity;

        TEntity Read<TEntity>(params object[] id) where TEntity : class, IEntity;

        IQueryable<TEntity> ReadAll<TEntity>() where TEntity : class, IEntity;

        bool TryRead<TEntity>(object[] id, out TEntity entity) where TEntity : class, IEntity;

        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
    }
}