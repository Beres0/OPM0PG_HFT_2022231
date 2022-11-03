using System;
using System.Collections.ObjectModel;

namespace OPM0PG_HFT_2022231.Client.Readers
{
    public class ConsoleTypeReaderCollection : KeyedCollection<Type, ConsoleTypeReader>
    {
        protected override Type GetKeyForItem(ConsoleTypeReader item)
        {
            return item.ReadingType;
        }
    }
}