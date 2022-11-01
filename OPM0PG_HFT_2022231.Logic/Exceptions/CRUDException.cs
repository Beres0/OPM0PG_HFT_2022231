using OPM0PG_HFT_2022231.Models;
using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public abstract class CRUDException : InvalidOperationException
    {
        public Type EntityType { get; }
        public object[] Id { get; }
        public IEntity Entity { get; }

        private CRUDException(Type entityType, string message, Exception innerException = null)
        : base(message, innerException)
        {
            EntityType = entityType;
        }

        protected CRUDException(Type entityType, string message, Exception innerException = null, params object[] id)
        : this(entityType, message, innerException)
        {
            Id = id;
        }

        protected CRUDException(IEntity entity, string message, Exception innerException = null)
            : this(entity?.GetType(), message, innerException, entity?.GetId())
        { }
    }
}