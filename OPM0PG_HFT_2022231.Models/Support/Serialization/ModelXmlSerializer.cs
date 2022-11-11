using System;
using System.IO;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models.Support.Serialization
{
    public static class ModelXmlSerializer<T>
    {
        private static XmlSerializer serializer = new XmlSerializer(typeof(T));

        public static T Deserialize(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, true))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException(path + " " + ex.Message, ex);
            }
        }

        public static void Serialize(string path, T obj)
        {
            using (var writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}