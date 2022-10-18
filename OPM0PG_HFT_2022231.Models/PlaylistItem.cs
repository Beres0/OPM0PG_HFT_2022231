using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace OPM0PG_HFT_2022231.Models
{
    [Index(nameof(PlaylistId), nameof(PlaylistNo), IsUnique = true)]
    public class PlaylistItem : IEntity<int>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(Playlist))]
        public int PlaylistId { get; set; }

        public int PlaylistNo { get; set; }

        [ForeignKey(nameof(Track))]
        public int SongId { get; set; }

        [JsonIgnore]
        public virtual Playlist Playlist { get; set; }

        [JsonIgnore]
        public virtual Track Song { get; set; }
    }
}