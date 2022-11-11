using System;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Client.Readers
{
    public abstract class ConsoleTypeReader
    {
        public ConsoleTypeReader(Type readingType)
        {
            ReadingType = readingType;
        }

        public Type ReadingType { get; }

        public object Read(Func<PropertyInfo,bool> ignore=null)
        {
            var obj = ReadMethod(ignore);
            if (obj.GetType() != ReadingType)
            {
                throw new NotSupportedException();
            }
            return obj;
        }

        protected abstract object ReadMethod(Func<PropertyInfo,bool> ignore);
    }
}