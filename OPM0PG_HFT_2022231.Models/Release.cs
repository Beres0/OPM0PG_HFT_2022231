using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Release : IEntity
    {
        public Release()
        {
            CollectionSetter<Release>.SetCollections(this);
        }

        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int? ReleaseYear { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        object[] IEntity.GetId() => new object[] { Id };
    }
}