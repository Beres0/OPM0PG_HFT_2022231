using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    [Index(nameof(Name), IsUnique = true), Index(nameof(Email), IsUnique = true)]
    public class User : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Sha { get; set; }

        public DateTime RegistrationDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Like> LikedSongs { get; set; }
    }
}