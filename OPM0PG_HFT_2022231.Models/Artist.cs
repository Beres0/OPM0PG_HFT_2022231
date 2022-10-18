using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Artist : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255), Required]
        public string Name { get; set; }

        [ForeignKey(nameof(Band))]
        public int? BandId { get; set; }

        [JsonIgnore]
        public virtual Artist Band { get; set; }

        [JsonIgnore, InverseProperty(nameof(Band))]
        public virtual ICollection<Artist> Members { get; set; }

        [JsonIgnore]
        public virtual ICollection<Contributor> ContributedAlbums { get; set; }
    }
}