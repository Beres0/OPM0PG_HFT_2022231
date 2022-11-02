using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class ReadException : CRUDException
    {
        public ReadException(Type entityType, Exception innerException = null, params object[] id)
            : base(entityType, ExceptionMessage(entityType, innerException, id), innerException, id)
        { }

        private static string ExceptionMessage(Type entityType, Exception innerException, object[] id)
        {
            return innerException is null ?
                $"Error in reading! [{entityType.Name} - ({(id is null ? "null" : string.Join(", ", id))})"
                : innerException.Message;
        }
    }
}