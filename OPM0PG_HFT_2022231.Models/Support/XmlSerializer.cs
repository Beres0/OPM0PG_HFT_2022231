using System;
using System.IO;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models.Support
{
    public class XmlSerializer<T>
    {
        private XmlSerializer serializer;

        public XmlSerializer()
        {
            serializer = new XmlSerializer(typeof(T));
        }

        public T Deserialize(string path)
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

        public void Serialize(string path, T obj)
        {
            using (var writer = new StreamWriter(path, false))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}