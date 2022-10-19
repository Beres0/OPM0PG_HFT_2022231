using OPM0PG_HFT_2022231.Models;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IRepository<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        void Create(TEntity item);

        IQueryable<TEntity> ReadAll();

        TEntity Read(TKey id);

        void Update(TEntity item);

        void Delete(TKey id);
    }
}