using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class Membership : IEntity
    {
        public bool Active { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        public int BandId { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Artist Member { get; set; }

        public int MemberId { get; set; }

        public object[] GetId() => new object[] { BandId, MemberId };
    }
}