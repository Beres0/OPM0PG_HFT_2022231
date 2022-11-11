using Newtonsoft.Json;
using OPM0PG_HFT_2022231.Models.Utility.Serialization.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Utility.Serialization
{
    public static class ModelJsonSerializer
    {
        private static Dictionary<Type, Converter> converters = CollectConverters();

        public static T Deserialize<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, GetConverters());
        }

        public static object Deserialize(string jsonString, Type returnType)
        {
            return JsonConvert.DeserializeObject(jsonString, returnType, GetConverters());
        }

        public static Converter[] GetConverters() => converters.Values.ToArray();

        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented, GetConverters());
        }

        public static void SetConverter(Converter converter)
        {
            converters[converter.ConversionType] = converter;
        }

        public static bool TryGetConverter(Type type, out Converter converter)
        {
            return converters.TryGetValue(type, out converter);
        }

        private static Dictionary<Type, Converter> CollectConverters()
        {
            Dictionary<Type, Converter> converters = new Dictionary<Type, Converter>();
            Type baseType = typeof(Converter);
            var types = Assembly.GetAssembly(baseType).GetTypes()
                        .Where(t => !t.IsAbstract && t.IsSubclassOf(baseType));
            foreach (var type in types)
            {
                Converter converter = (Converter)(Activator.CreateInstance(type));
                converters.Add(converter.ConversionType, converter);
            }
            return converters;
        }
    }
}