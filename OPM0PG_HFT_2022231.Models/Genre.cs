using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Genre : IEntity<object>
    {
        [Key, Column(Order = 0)]
        public string GenreType { get; set; }

        [Key, Column(Order = 1), ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [NotMapped, XmlIgnore, JsonIgnore]
        object IEntity<object>.Id => new { GenreType, AlbumId };
    }
}