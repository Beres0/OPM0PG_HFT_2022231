using System.Text.Json.Serialization;
using System.Xml.Serialization;
using OPM0PG_HFT_2022231.Models.Internals;

namespace OPM0PG_HFT_2022231.Models
{
    public class Contribution : IEntity
    {
        public Contribution()
        {
            CollectionSetter<Contribution>.SetCollections(this);
        }

        public int AlbumId { get; set; }

        public int ArtistId { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Artist { get; set; }

        object[] IEntity.GetId() => new object[] { AlbumId, ArtistId };
    }
}