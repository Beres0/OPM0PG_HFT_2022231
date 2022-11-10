using System.Xml.Serialization;

using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class AlbumMedia : Media
    {
        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        public int AlbumId { get; set; }
    }
}