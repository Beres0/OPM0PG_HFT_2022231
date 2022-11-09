using Newtonsoft.Json;
using OPM0PG_HFT_2022231.Models.DataTransferObjects;
using System;

namespace OPM0PG_HFT_2022231.Models.Support.JsonConverters
{
    public class HttpMethodTypeConverter : JsonConverter<HttpMethodType>
    {
        public override HttpMethodType ReadJson(JsonReader reader, Type objectType, HttpMethodType existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (Enum.TryParse(reader.Value.ToString(), true, out HttpMethodType result))
            {
                return result;
            }
            else return HttpMethodType.None;
        }

        public override void WriteJson(JsonWriter writer, HttpMethodType value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}