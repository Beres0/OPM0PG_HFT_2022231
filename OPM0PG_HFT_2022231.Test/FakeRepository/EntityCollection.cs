using OPM0PG_HFT_2022231.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OPM0PG_HFT_2022231.Test.Repository
{
    public class EntityCollection<TEntity> : KeyedCollection<object[], TEntity>
        where TEntity : class, IEntity
    {
        public EntityCollection() : base(new KeyEqualityComparer())
        {
        }

        public EntityCollection(IEnumerable<TEntity> entities) : this()
        {
            foreach (var item in entities)
            {
                if (!Contains(item))
                {
                    Add(item);
                }
            }
        }

        protected override object[] GetKeyForItem(TEntity item)
        {
            return item.GetId();
        }

        private class KeyEqualityComparer : IEqualityComparer<object[]>
        {
            public bool Equals(object[] x, object[] y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode([DisallowNull] object[] obj)
            {
                unchecked
                {
                    int hash = 171923;
                    for (int i = 0; i < obj.Length; i++)
                    {
                        hash *= 314159 ^ obj[i].GetHashCode();
                    }
                    return hash;
                }
            }
        }
    }
}