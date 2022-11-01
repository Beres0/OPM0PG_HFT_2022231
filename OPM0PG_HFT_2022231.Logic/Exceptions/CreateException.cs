using OPM0PG_HFT_2022231.Models;
using System;

namespace OPM0PG_HFT_2022231.Logic.Validating.Exceptions
{
    public class CreateException : CRUDException
    {
        public CreateException(IEntity entity, Exception innerException = null)
          : base(entity, $"Error in creating!" + (entity is null ? "Given entity is null!" : $"Type: {entity?.GetType().Name} Key: ({string.Join(", ", entity?.GetId())})"), innerException)
        { }
    }
}