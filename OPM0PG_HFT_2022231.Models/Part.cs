using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Part : IEntity<object>
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

    public class Part1 : IEntity
    {
        public Part1()
        {
            CollectionSetter<Part1>.SetCollections(this);
        }

        public int AlbumId { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album1 Album { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Track1> Tracks { get; set; }

        object[] IEntity.GetId() => new object[] {AlbumId, Id };
    }
}