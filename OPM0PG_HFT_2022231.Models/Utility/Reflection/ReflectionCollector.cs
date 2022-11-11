using System;
using System.Linq;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Utility.Reflection
{
    public class ReflectionCollector
    {
        public static Type[] CollectEntityTypes()
        {
            Type type = typeof(IEntity);
            return Assembly.GetAssembly(type)
                    .GetTypes()
                    .Where((t) => !t.IsAbstract && !t.IsInterface && t.GetInterface(type.Name) is not null).ToArray();
        }

        public static PropertyInfo[] CollectNonVirtualProperties<T>()
        {
            return typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => !p.SetMethod.IsVirtual && p.CanWrite).ToArray();
        }
    }
}