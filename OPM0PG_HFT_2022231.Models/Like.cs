using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Like : IEntity<object>
    {
        [Key, Column(Order = 0), ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [Key, Column(Order = 1), ForeignKey(nameof(Song))]
        public int SongId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }

        [JsonIgnore]
        public virtual Track Song { get; set; }

        [NotMapped, JsonIgnore]
        object IEntity<object>.Id => new { UserId, SongId };
    }
}