using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IRepository<TKey, TEntity> where TEntity : class, IEntity<TKey>
    {
        void Create(TEntity item);
        TEntity Read(TKey id);
        IEnumerable<TEntity> ReadAll();
        void Update(TEntity item);
        void Delete(TKey id);
    }
}