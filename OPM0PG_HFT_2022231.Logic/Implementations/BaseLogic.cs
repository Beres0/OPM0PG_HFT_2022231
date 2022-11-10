using OPM0PG_HFT_2022231.Logic.Validating.Exceptions;
using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
    public abstract class BaseLogic
    {
        protected readonly IMusicRepository repository;

        protected BaseLogic(IMusicRepository repository)
        {
            this.repository = repository;
        }

        protected void CheckKeyAlreadyAdded<TEntity>(params object[] id)
            where TEntity : class, IEntity
        {
            if (repository.TryRead(id, out TEntity added))
            {
                throw new InvalidOperationException($"The given entity has already been added! Key: ({string.Join(", ", id)})");
            }
        }

        protected void CheckKeyExists<TEntity>(params object[] id)
                    where TEntity : class, IEntity
        {
            if (!repository.TryRead(id, out TEntity entity))
            {
                throw new ArgumentException($"The given key id is not found! Key: ({string.Join(", ", id)})");
            }
        }

        protected void CreateEntity<TEntity>(Action tryAction, TEntity entity)
           where TEntity : class, IEntity
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                tryAction?.Invoke();
                repository.Create(entity);
            }
            catch (Exception ex)
            {
                throw new CreateException(entity, ex);
            }
        }

        protected void DeleteEntity<TEntity>(Action tryAction, params object[] id)
           where TEntity : class, IEntity
        {
            try
            {
                CheckKeyExists<TEntity>(id);
                tryAction?.Invoke();
                repository.Delete<TEntity>(id);
            }
            catch (Exception ex)
            {
                throw new DeleteException(typeof(Album), ex, id);
            }
        }

        protected void DeleteEntityWithSimpleNumericKey<TEntity>(int id)
           where TEntity : class, IEntity
        {
            DeleteEntity<TEntity>(null, id);
        }

        protected TResult QueryRead<TResult>(Func<TResult> tryQuery)
        {
            try
            {
                return tryQuery();
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(Album), ex, null);
            }
        }

        protected TEntity ReadEntity<TEntity>(Action tryAction, params object[] id)
                                           where TEntity : class, IEntity
        {
            try
            {
                CheckKeyExists<TEntity>(id);
                tryAction?.Invoke();
                return repository.Read<TEntity>(id);
            }
            catch (Exception ex)
            {
                throw new ReadException(typeof(TEntity), ex, id);
            }
        }

        protected TEntity ReadEntityWithSimpleNumericKey<TEntity>(int id)
        where TEntity : class, IEntity
        {
            return ReadEntity<TEntity>(null, id);
        }

        protected void UpdateEntity<TEntity>(Action tryAction, TEntity entity)
           where TEntity : class, IEntity
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                CheckKeyExists<TEntity>(entity.GetId());
                tryAction?.Invoke();
                repository.Update(entity);
            }
            catch (Exception ex)
            {
                throw new UpdateException(entity, ex);
            }
        }
    }
}