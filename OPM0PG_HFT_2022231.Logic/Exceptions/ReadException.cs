using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class ReadException : CRUDException
    {
        public ReadException(Type entityType, Exception innerException = null, params object[] id)
            : base(entityType, $"Error in reading! Type: {entityType.Name} Key: ({(id is null ? "null" : string.Join(", ", id))})", innerException, id)
        { }
    }
}