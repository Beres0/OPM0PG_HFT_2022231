using OPM0PG_HFT_2022231.Models.Support;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Membership : IEntity
    {
        public bool Active { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        [Range(0, int.MaxValue)]
        public int BandId { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Member { get; set; }

        [Range(0, int.MaxValue)]
        public int MemberId { get; set; }

        public object[] GetId() => new object[] { BandId, MemberId };
    }
}