using Newtonsoft.Json;
using System;
using System.Globalization;

namespace OPM0PG_HFT_2022231.Models.Support.JsonConverters
{
    public class DurationJsonConverter : JsonConverter<TimeSpan>
    {
        private string format = @"hh\:mm\:ss";

        public DurationJsonConverter()
        {
        }

        public override TimeSpan ReadJson(JsonReader reader, Type objectType, TimeSpan existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            TimeSpan.TryParseExact(reader.Value.ToString(), format, CultureInfo.CurrentCulture, out TimeSpan result);
            return result;
        }

        public override void WriteJson(JsonWriter writer, TimeSpan value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString(format));
        }
    }
}