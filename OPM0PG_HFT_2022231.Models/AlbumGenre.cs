using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class AlbumGenre : IEntity<object>
    {
        [Key, Column(Order = 0)]
        public string Genre { get; set; }

        [Key, Column(Order = 1), ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        [JsonIgnore]
        public virtual Album Album { get; set; }

        [NotMapped, JsonIgnore]
        object IEntity<object>.Id => new { Genre, AlbumId };
    }
}