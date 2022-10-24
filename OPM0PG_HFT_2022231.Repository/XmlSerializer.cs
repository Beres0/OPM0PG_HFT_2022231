using System.IO;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class XmlSerializer<T>
    {
        private XmlSerializer serializer;

        public XmlSerializer()
        {
            serializer = new XmlSerializer(typeof(T));
        }

        public void Serialize(string path, T obj)
        {
            using (var writer = new StreamWriter(path, true))
            {
                serializer.Serialize(writer, obj);
            }
        }

        public T Deserialize(string path)
        {
            using (var reader = new StreamReader(path, true))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}