using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Album : IEntity<int>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }

        public Album()
        {
            CollectionSetter<Album>.SetCollections(this);
        }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Contribution> Contributions { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<AlbumGenre> Genres { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Part> Parts { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Release> Releases { get; set; }
    }
}