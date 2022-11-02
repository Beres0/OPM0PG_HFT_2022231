using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Runtime.CompilerServices;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public abstract class BaseLogic
    {
        protected readonly IMusicRepository repository;

        protected BaseLogic(IMusicRepository repository)
        {
            this.repository = repository;
        }

        protected void CheckKeyExists<TEntity>(IRepository<TEntity> repository, string argName, params object[] id)
            where TEntity : class, IEntity
        {
            if (!repository.TryRead(id, out TEntity entity))
            {
                throw new ArgumentException($"The given key '{argName}' is not found! Key: ({string.Join(", ", id)})");
            }
        }

        protected void CheckKeyExists<TEntity>(IRepository<TEntity> repository, int id, [CallerArgumentExpression("id")] string argName = null)
         where TEntity : class, IEntity
        {
            CheckKeyExists(repository, argName, id);
        }

        protected void CheckKeyAlreadyAdded<TEntity>(IRepository<TEntity> repository, string argName, params object[] id)
            where TEntity : class, IEntity
        {
            if (repository.TryRead(id, out TEntity added))
            {
                throw new InvalidOperationException($"The given entity has already been added! Key: ({string.Join(", ", id)})");
            }
        }
    }
}