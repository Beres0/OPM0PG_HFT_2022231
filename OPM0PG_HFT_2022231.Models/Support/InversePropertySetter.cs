using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Support
{
    internal static class InversePropertySetter<TEntity>
    {
        private static readonly Type CollectionType = typeof(HashSet<>);
        private static readonly Action<TEntity>[] CollectionSetters = CreateCollectionSetters();



        private static Action<TEntity>[] CreateCollectionSetters()
        {
            Type entityType = typeof(TEntity);

            var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.SetMethod.IsVirtual && p.PropertyType.Name.Contains("ICollection")).ToArray();

            var entity = Expression.Parameter(entityType, "entity");

            Action<TEntity>[] collectionSetters = new Action<TEntity>[props.Length];

            for (int i = 0; i < props.Length; i++)
            {
                collectionSetters[i] = Expression.Lambda<Action<TEntity>>(
                    Expression.Assign(Expression.Property(entity, props[i].Name),
                                      Expression.New(GetCollectionConstructorInfo(props[i]))), entity)
                    .Compile();
            }
            return collectionSetters;
        }
        private static ConstructorInfo GetCollectionConstructorInfo(PropertyInfo prop)
        {
            Type genericParameter = prop.PropertyType.GetGenericArguments()[0];
            Type genericCollectionType = CollectionType.GetGenericTypeDefinition().MakeGenericType(genericParameter);
            return genericCollectionType.GetConstructor(new Type[] { });
        }
        public static void SetCollections(TEntity entity)
        {
            foreach (var setter in CollectionSetters)
            {
                setter(entity);
            }
        }
    }
}
