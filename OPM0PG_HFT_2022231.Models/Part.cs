using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Part : IEntity<object>
    {
        [Key, Column(Order = 0), ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        [Key, Column(Order = 1)]
        public int Id { get; set; }

        [StringLength(255), Required]
        public string Title { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        [JsonIgnore, XmlIgnore, NotMapped]
        object IEntity<object>.Id => new { AlbumId, Id };
    }
}