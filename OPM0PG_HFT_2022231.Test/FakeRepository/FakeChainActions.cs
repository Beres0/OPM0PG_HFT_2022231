using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test.Repository
{
    public class FakeChainActions<TEntity> : IRepositoryChainActions<TEntity>
        where TEntity : class, IEntity
    {
        private Action actions;
        private EntityCollection<TEntity> source;

        public FakeChainActions(EntityCollection<TEntity> source)
        {
            this.source = source;
        }

        public IRepositoryChainActions<TEntity> CreateWithoutSave(IEnumerable<TEntity> entities)
        {
            return AddAction(() =>
            {
                foreach (var item in entities)
                {
                    source.Add(item);
                }
            });
        }

        public IRepositoryChainActions<TEntity> CreateWithoutSave(params TEntity[] entities)
        {
            return CreateWithoutSave((IEnumerable<TEntity>)entities);
        }

        public IRepositoryChainActions<TEntity> DeleteWithoutSave(IEnumerable<TEntity> entities)
        {
            return AddAction(() =>
            {
                foreach (var item in entities)
                {
                    source.Remove(item);
                }
            });
        }

        public IRepositoryChainActions<TEntity> DeleteWithoutSave(params TEntity[] entities)
        {
            return DeleteWithoutSave((IEnumerable<TEntity>)entities);
        }

        public IRepositoryChainActions<TEntity> Read(out TEntity entity, params object[] id)
        {
            entity = source[id];
            return this;
        }

        public IRepositoryChainActions<TEntity> ReadWhere(Func<TEntity, bool> predicate, out IEnumerable<TEntity> result)
        {
            result = source.Where(predicate);
            return this;
        }

        public void Save()
        {
            if (actions != null)
            {
                actions();
                actions = null;
            }
        }

        public IRepositoryChainActions<TEntity> UpdateWithoutSave(IEnumerable<TEntity> entities, Action<TEntity> updateAction)
        {
            return AddAction(() =>
             {
                 foreach (var item in entities.Select(e => source[e.GetId()]))
                 {
                     updateAction(item);
                 }
             });
        }

        public IRepositoryChainActions<TEntity> UpdateWithoutSave(TEntity item, Action<TEntity> updateAction)
        {
            return AddAction(() =>
            {
                updateAction(source[item.GetId()]);
            });
        }

        public IRepositoryChainActions<TEntity> UpdateWithoutSave(TEntity entity)
        {
            return AddAction(() =>
            {
                source.Remove(entity.GetId());
                source.Add(entity);
            });
        }

        private IRepositoryChainActions<TEntity> AddAction(Action action)
        {
            actions += action;
            return this;
        }
    }
}