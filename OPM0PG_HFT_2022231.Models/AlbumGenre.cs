using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class AlbumGenre : IEntity<object>
    {
        public int AlbumId { get; set; }
        public string Genre { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [NotMapped, XmlIgnore, JsonIgnore]
        object IEntity<object>.Id => new {AlbumId,Genre };
    }
}