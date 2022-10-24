using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Track : IEntity<object>
    {
        public int AlbumId { get; set; }

        public int PartId { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public TimeSpan? Duration { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Part Part { get; set; }

        [JsonIgnore, XmlIgnore, NotMapped]
        object IEntity<object>.Id => new { AlbumId, PartId, Id };
    }
}