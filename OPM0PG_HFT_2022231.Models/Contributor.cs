using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Contributor
    {
        [Key, Column(Order = 0), ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        [Key, Column(Order = 1), ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }

        [JsonIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore]
        public virtual Artist Artist { get; set; }
    }
}