using Newtonsoft.Json;
using System;
using System.Globalization;

public class NullableDurationJsonConverter : JsonConverter<TimeSpan?>
{
    private string format = @"hh\:mm\:ss";

    public NullableDurationJsonConverter()
    {
    }

    public override TimeSpan? ReadJson(JsonReader reader, Type objectType, TimeSpan? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        TimeSpan.TryParseExact(reader.Value.ToString(), format, CultureInfo.CurrentCulture, out TimeSpan result);
        return result;
    }

    public override void WriteJson(JsonWriter writer, TimeSpan? value, JsonSerializer serializer)
    {
        writer.WriteValue(value.HasValue ? value.Value.ToString(format) : null);
    }
}