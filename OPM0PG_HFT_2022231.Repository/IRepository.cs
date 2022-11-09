using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System.Linq;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IRepositoryChainActions<TEntity> ChainActions();

        void Create(TEntity item);

        void Delete(params object[] id);

        TEntity Read(params object[] id);

        IQueryable<TEntity> ReadAll();

        bool TryRead(object[] id, out TEntity entity);

        void Update(TEntity item);
    }
}