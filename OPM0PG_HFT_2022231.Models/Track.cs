using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Track : IEntity<object>
    {
        [Key, Column(Order = 0), ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        [Key, Column(Order = 1), ForeignKey(nameof(AlbumPart))]
        public int PartId { get; set; }

        [Key, Column(Order = 2)]
        public int Id { get; set; }

        [Required,StringLength(255)]
        public string Title { get; set; }

        public TimeSpan? Duration { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Part AlbumPart { get; set; }

        [JsonIgnore, XmlIgnore, NotMapped]
        object IEntity<object>.Id => new { AlbumId, PartId, Id };
    }
}