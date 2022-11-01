using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Test.Repository
{
    public class FakeRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private EntityCollection<TEntity> original;
        private EntityCollection<TEntity> entities;

        public FakeRepository()
        {
            entities = new EntityCollection<TEntity>();
        }

        public FakeRepository(IEnumerable<TEntity> beginState)
        {
            original = new EntityCollection<TEntity>(beginState);
            entities = new EntityCollection<TEntity>();
            Reset();
        }

        public void Reset()
        {
            if (entities.Count > 0) entities.Clear();
            if (original != null)
            {
                foreach (var item in original)
                {
                    TEntity copy = EntityCopier<TEntity>.CopyToNew(item);
                    entities.Add(copy);
                }
            }
        }

        public IRepositoryChainActions<TEntity> ChainActions()
        {
            return new FakeChainActions<TEntity>(entities);
        }

        public bool ContainsKey(object[] id, out TEntity entity)
        {
            return entities.TryGetValue(id, out entity);
        }

        public void Create(TEntity item)
        {
            entities.Add(item);
        }

        public void Delete(params object[] id)
        {
            entities.Remove(id);
        }

        public TEntity Read(params object[] id)
        {
            return entities[id];
        }

        public IEnumerable<TEntity> ReadAll()
        {
            return entities;
        }

        public bool TryRead(object[] id, out TEntity entity)
        {
            return entities.TryGetValue(id, out entity);
        }

        public void Update(TEntity item)
        {
            entities.Remove(item.GetId());
            entities.Add(item);
        }
    }
}