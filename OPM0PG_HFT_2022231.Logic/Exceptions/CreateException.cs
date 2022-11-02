using OPM0PG_HFT_2022231.Models;
using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class CreateException : CRUDException
    {
        private static string ExceptionMessage(IEntity entity, Exception innerException)
        {
            return innerException is null ?
            $"Error in updating! [{(entity is null ? "NULL" : $"{entity.GetType().Name} - ({string.Join(", ", entity.GetId())})")}]" : innerException.Message;
        }
        public CreateException(IEntity entity, Exception innerException = null)
          : base(entity, ExceptionMessage(entity,innerException), innerException)
        { }
    }
}