using System;
using System.Collections.ObjectModel;

namespace OPM0PG_HFT_2022231.Client.Writers
{
    public class ConsoleTypeWriterCollection : KeyedCollection<Type, ConsoleTypeWriter>
    {
        protected override Type GetKeyForItem(ConsoleTypeWriter item)
        {
            return item.WritingType;
        }
    }
}