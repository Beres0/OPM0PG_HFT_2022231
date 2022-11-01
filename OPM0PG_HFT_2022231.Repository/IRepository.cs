﻿using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository.ChainActions;
using System.Collections.Generic;

namespace OPM0PG_HFT_2022231.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        void Create(TEntity item);

        TEntity Read(params object[] id);

        IEnumerable<TEntity> ReadAll();

        void Update(TEntity item);

        void Delete(params object[] id);

        bool TryRead(object[] id, out TEntity entity);

        IRepositoryChainActions<TEntity> ChainActions();
    }
}