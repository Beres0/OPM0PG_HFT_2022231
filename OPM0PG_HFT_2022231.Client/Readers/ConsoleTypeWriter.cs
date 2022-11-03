using System;

namespace OPM0PG_HFT_2022231.Client.ConsoleTypeWriter
{
    public abstract class ConsoleTypeWriter
    {
        public Type WritingType { get; }
        public ConsoleTypeWriter(Type writingType)
        {
            WritingType = writingType;
        }
        public abstract void Write(object obj);
    }
}