using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Artist : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Artist()
        {
            CollectionSetter<Artist>.SetCollections(this);
        }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Membership> Members { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Membership> Bands { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Contribution> ContributedAlbums { get; set; }

        object[] IEntity.GetId() => new object[] { Id };
    }
}