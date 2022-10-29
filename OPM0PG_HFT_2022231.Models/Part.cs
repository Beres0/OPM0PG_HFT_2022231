using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Part : IEntity
    {
        public Part()
        {
            CollectionSetter<Part>.SetCollections(this);
        }

        public int AlbumId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        object[] IEntity.GetId() => new object[] { Id };
    }
}