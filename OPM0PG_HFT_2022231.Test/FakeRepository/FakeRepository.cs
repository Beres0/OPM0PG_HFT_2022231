using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using OPM0PG_HFT_2022231.Repository;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test.Repository
{
    public class FakeRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private EntityCollection<TEntity> entities;
        private EntityCollection<TEntity> original;

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

        public IRepositoryChainActions<TEntity> ChainActions()
        {
            return new FakeChainActions<TEntity>(entities);
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

        public IQueryable<TEntity> ReadAll()
        {
            return entities.AsQueryable();
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