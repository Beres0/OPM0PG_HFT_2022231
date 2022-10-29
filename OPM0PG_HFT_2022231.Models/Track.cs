using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Track : IEntity
    {
        public Track()
        {
            CollectionSetter<Track>.SetCollections(this);
        }

        public int PartId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public TimeSpan? Duration { get; set; }


        [JsonIgnore, XmlIgnore]
        public virtual Part Part { get; set; }

        object[] IEntity.GetId() => new object[] { Id };

    }
}