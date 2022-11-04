﻿using System;

namespace OPM0PG_HFT_2022231.Client.Readers
{
    public abstract class ConsoleTypeReader
    {
        public ConsoleTypeReader(Type readingType)
        {
            ReadingType = readingType;
        }

        public Type ReadingType { get; }

        public object Read()
        {
            var obj = ReadMethod();
            if (obj.GetType() != ReadingType)
            {
                throw new NotSupportedException();
            }
            return obj;
        }

        protected abstract object ReadMethod();
    }
}