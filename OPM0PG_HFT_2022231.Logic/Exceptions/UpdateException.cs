using OPM0PG_HFT_2022231.Models;
using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class UpdateException : CRUDException
    {
        public UpdateException(IEntity entity, Exception innerException = null)
             : base(entity, ExecptionMessage(entity, innerException), innerException)

        { }

        private static string ExecptionMessage(IEntity entity, Exception innerException)
        {
            return innerException is null ?
            $"Error in updating! [{(entity is null ? "NULL" : $"{entity.GetType().Name} - ({string.Join(", ", entity.GetId())})")}]" : innerException.Message;
        }
    }
}