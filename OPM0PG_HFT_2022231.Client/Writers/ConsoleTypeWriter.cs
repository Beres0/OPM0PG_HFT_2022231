﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OPM0PG_HFT_2022231.Client.Writers
{
    public abstract class ConsoleTypeWriter
    {
        public ConsoleTypeWriter(Type writingType)
        {
            WritingType = writingType;
        }

        public Type WritingType { get; }

        public void Write(object obj)
        {
            if (obj.GetType() != WritingType)
            {
                throw new NotSupportedException();
            }
            WriteMethod(obj);
        }

        protected void WriteCollection<T>(string title, IEnumerable<T> items, Func<T, string> toString)
        {
            Console.WriteLine(title + ":");
            Console.WriteLine(string.Join("\n", items.Select(i => "\t" + toString(i))));
            Console.WriteLine();
        }

        protected abstract void WriteMethod(object obj);
    }
}