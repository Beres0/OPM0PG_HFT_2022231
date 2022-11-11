using Newtonsoft.Json;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;

namespace OPM0PG_HFT_2022231.Models.Utility.Serialization.Converters
{
    public abstract class EnumConverter<TEnum> : Converter
        where TEnum : struct, Enum
    {
        public override Type ConversionType { get; } = typeof(TEnum);

        public override object Convert(string value)
        {
            return Enum.Parse<TEnum>(value, true);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return Convert(reader.Value.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }

    public class HttpMethodTypeConverter : EnumConverter<HttpMethodType>
    {
    }

    public class MediaTypeConverter : EnumConverter<MediaType>
    {
    }
}