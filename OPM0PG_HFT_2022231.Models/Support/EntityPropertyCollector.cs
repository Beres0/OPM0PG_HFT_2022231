using System.Linq;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Support
{
    internal class EntityPropertyCollector<TEntity>
          where TEntity : class, IEntity
    {
        public static PropertyInfo[] CollectProperties()
        {
            return typeof(TEntity).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !p.SetMethod.IsVirtual && p.CanWrite).ToArray();
        }
    }
}