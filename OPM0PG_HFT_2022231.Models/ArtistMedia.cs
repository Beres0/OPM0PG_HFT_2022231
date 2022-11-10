using System.Xml.Serialization;
using NativeJson = System.Text.Json;
using NewtonsoftJson = Newtonsoft.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class ArtistMedia : Media
    {
        [NativeJson.Serialization.JsonIgnore, NewtonsoftJson.JsonIgnore, XmlIgnore]
        public virtual Artist Artist { get; set; }

        public int ArtistId { get; set; }
    }
}