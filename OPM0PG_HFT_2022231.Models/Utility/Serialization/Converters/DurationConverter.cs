using Newtonsoft.Json;
using System;
using System.Globalization;

namespace OPM0PG_HFT_2022231.Models.Utility.Serialization.Converters
{
    public abstract class DurationConverter : Converter
    {
        protected string format = @"hh\:mm\:ss";

        public override Type ConversionType => typeof(TimeSpan);

        public override object Convert(string value)
        {
            return TimeSpan.ParseExact(value, format, CultureInfo.CurrentCulture);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((TimeSpan)value).ToString(format));
        }
    }
}