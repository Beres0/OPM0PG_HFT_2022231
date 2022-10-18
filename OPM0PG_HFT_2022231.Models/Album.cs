using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Album : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255), Required]
        public string Title { get; set; }

        [Range(1800, 2030)]
        public int ReleaseYear { get; set; }

        public ReleaseType ReleaseType { get; set; }

        [ForeignKey(nameof(Publisher))]
        public int PublisherId { get; set; }

        [JsonIgnore]
        public virtual Publisher Publisher { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contributor> Contributors { get; set; }

        [JsonIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        [JsonIgnore]
        public virtual ICollection<AlbumGenre> Genres { get; set; }
    }
}