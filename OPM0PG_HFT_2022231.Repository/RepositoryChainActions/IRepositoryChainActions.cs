using OPM0PG_HFT_2022231.Models;
using System;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Repository.RepositoryChainActions
{
    public interface IRepositoryChainActions<TEntity> where TEntity : class, IEntity
    {
        IRepositoryChainActions<TEntity> CreateWithoutSave(IEnumerable<TEntity> entities);
        IRepositoryChainActions<TEntity> CreateWithoutSave(params TEntity[] entities);
        IRepositoryChainActions<TEntity> DeleteWithoutSave(IEnumerable<TEntity> entities);
        IRepositoryChainActions<TEntity> DeleteWithoutSave(params TEntity[] entities);
        IRepositoryChainActions<TEntity> Read(out TEntity entity, params object[] id);
        IRepositoryChainActions<TEntity> ReadWhere(Func<TEntity, bool> predicate, out IEnumerable<TEntity> entities);
        void Save();
        IRepositoryChainActions<TEntity> UpdateWithoutSave(IEnumerable<TEntity> entities, Action<TEntity> updateAction);
        IRepositoryChainActions<TEntity> UpdateWithoutSave(TEntity item, Action<TEntity> updateAction);
        IRepositoryChainActions<TEntity> UpdateWithoutSave(TEntity entity);
    }
}