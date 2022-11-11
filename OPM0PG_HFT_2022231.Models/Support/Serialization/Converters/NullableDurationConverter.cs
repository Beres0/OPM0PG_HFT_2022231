using Newtonsoft.Json;
using System;
using System.Globalization;

namespace OPM0PG_HFT_2022231.Models.Support.Serialization.Converters
{
    public class NullableDurationConverter : DurationConverter
    {
        public override Type ConversionType { get; } = typeof(TimeSpan?);

        public override object Convert(string value)
        {
            if (TimeSpan.TryParseExact(value, format, CultureInfo.CurrentCulture, out TimeSpan result))
            {
                return result;
            }
            else return null;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is TimeSpan)
            {
                base.WriteJson(writer, value, serializer);
            }
        }
    }
}