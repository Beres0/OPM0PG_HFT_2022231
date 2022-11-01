using OPM0PG_HFT_2022231.Models.Support;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Membership : IEntity
    {
        public Membership()
        {
            InversePropertySetter<Membership>.SetCollections(this);
        }

        [Range(0, int.MaxValue)]
        public int BandId { get; set; }

        [Range(0, int.MaxValue)]
        public int MemberId { get; set; }

        public bool Active { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Member { get; set; }

        object[] IEntity.GetId() => new object[] { BandId, MemberId };
    }
}