using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    [Index(nameof(No), nameof(AlbumId), IsUnique = true)]
    public class Track : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string Title { get; set; }

        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }

        public int No { get; set; }
        public TimeSpan Duration { get; set; }

        [JsonIgnore]
        public virtual Album Album { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like> Likes { get; set; }
    }
}