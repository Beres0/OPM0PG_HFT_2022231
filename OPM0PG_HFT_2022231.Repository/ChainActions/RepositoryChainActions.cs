using Microsoft.EntityFrameworkCore;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository.ChainActions
{
    public class RepositoryChainActions<TEntity> : IRepositoryChainActions<TEntity> where TEntity : class, IEntity
    {
        private DbContext context;

        public RepositoryChainActions(DbContext context)
        {
            this.context = context;
        }

        public IRepositoryChainActions<TEntity> CreateWithoutSave(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().AddRange(entities);
            return this;
        }

        public IRepositoryChainActions<TEntity> CreateWithoutSave(params TEntity[] entities)
        {
            return CreateWithoutSave((IEnumerable<TEntity>)entities);
        }

        public IRepositoryChainActions<TEntity> DeleteWithoutSave(IEnumerable<TEntity> entities)
        {
            context.Set<TEntity>().RemoveRange(entities);
            return this;
        }

        public IRepositoryChainActions<TEntity> DeleteWithoutSave(params TEntity[] entities)
        {
            return DeleteWithoutSave((IEnumerable<TEntity>)entities);
        }

        public IRepositoryChainActions<TEntity> Read(out TEntity entity, params object[] id)
        {
            entity = context.Set<TEntity>().Find(id);
            return this;
        }

        public IRepositoryChainActions<TEntity> ReadWhere(Func<TEntity, bool> predicate, out IEnumerable<TEntity> result)
        {
            result = context.Set<TEntity>().Where(predicate);
            return this;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IRepositoryChainActions<TEntity> UpdateWithoutSave(TEntity entity, Action<TEntity> updateAction)
        {
            updateAction(entity);
            return this;
        }

        public IRepositoryChainActions<TEntity> UpdateWithoutSave(TEntity entity)
        {
            TEntity source = context.Set<TEntity>().Find(entity.GetId());
            EntityCopier<TEntity>.Copy(source, entity);

            return this;
        }

        public IRepositoryChainActions<TEntity> UpdateWithoutSave(IEnumerable<TEntity> entities, Action<TEntity> updateAction)
        {
            foreach (var item in entities)
            {
                updateAction(item);
            }
            return this;
        }
    }
}