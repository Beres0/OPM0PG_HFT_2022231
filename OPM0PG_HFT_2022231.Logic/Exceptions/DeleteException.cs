using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class DeleteException : CRUDException
    {
        public DeleteException(Type entityType, Exception innerException = null, params object[] id)
          : base(entityType, ExceptionMessage(entityType, innerException, id), innerException)
        { }

        private static string ExceptionMessage(Type entityType, Exception innerException, object[] id)
        {
            return innerException is null ?
                $"Error in deleting! [{entityType.Name} - ({(id is null ? "null" : string.Join(", ", id))})"
                : innerException.Message;
        }
    }
}