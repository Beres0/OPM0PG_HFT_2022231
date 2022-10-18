using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Playlist : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [ForeignKey(nameof(Creator))]
        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User Creator { get; set; }

        [JsonIgnore]
        public virtual ICollection<PlaylistItem> PlaylistItems { get; set; }
    }
}