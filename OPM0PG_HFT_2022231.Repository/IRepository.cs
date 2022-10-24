using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
    {
        void Create(TEntity item);

        void Delete(TKey id);

        void DeleteRange(IEnumerable<TKey> ids);

        TEntity Read(TKey id);

        IQueryable<TEntity> ReadAll();

        IQueryable<TEntity> ReadRange(IEnumerable<TKey> ids);

        void Update(TEntity item);
    }
}