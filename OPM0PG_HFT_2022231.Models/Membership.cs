using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Membership : IEntity
    {
        public Membership()
        {
            CollectionSetter<Membership>.SetCollections(this);
        }


        public int BandId { get; set; }
        public int MemberId { get; set; }
        public bool Active { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Member { get; set; }
        object[] IEntity.GetId() => new object[] {BandId,MemberId };


    }
}