using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Genre : IEntity<object>
    {
        public string GenreType { get; set; }
        public int AlbumId { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [NotMapped, XmlIgnore, JsonIgnore]
        object IEntity<object>.Id => new { GenreType, AlbumId };
    }
}