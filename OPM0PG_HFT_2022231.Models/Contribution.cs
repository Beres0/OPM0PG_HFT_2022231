using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Contribution : IEntity<object>
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

        [JsonIgnore, XmlIgnore, NotMapped]
        object IEntity<object>.Id => new { AlbumId, ArtistId };
    }
}