using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Member : IEntity<object>
    {
        public int BandId { get; set; }
        public int BandMemberId { get; set; }
        public bool Active { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist BandMember { get; set; }

        [JsonIgnore, XmlIgnore, NotMapped]
        object IEntity<object>.Id => new { BandId, BandMemberId };
    }
}