using Newtonsoft.Json;
using System;

namespace OPM0PG_HFT_2022231.Models.Support.Serialization.Converters
{
    public abstract class Converter : JsonConverter
    {
        public abstract Type ConversionType { get; }

        public override bool CanConvert(Type objectType)
        {
            return objectType == ConversionType;
        }

        public abstract object Convert(string value);
    }
}