using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Endpoint.JsonConverters
{
    public class DurationJsonConverter : JsonConverter<TimeSpan>
    {
        private string format = @"hh\:mm\:ss";

        public DurationJsonConverter()
        { }

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (TimeSpan.TryParseExact(reader.GetString(), format, CultureInfo.CurrentCulture, out TimeSpan result))
            {
                return result;
            }
            return TimeSpan.Zero;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(format));
        }
    }
}