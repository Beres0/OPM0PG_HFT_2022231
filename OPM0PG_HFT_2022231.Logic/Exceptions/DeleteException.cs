using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class DeleteException : CRUDException
    {
        public DeleteException(Type entityType, Exception innerException = null, params object[] id)
          : base(entityType, $"Error in deleting! Type: {entityType.Name} Key: ({(id is null ? "null" : string.Join(", ", id))})", innerException)
        { }
    }
}