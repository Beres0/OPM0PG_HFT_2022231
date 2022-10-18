using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Publisher : IEntity<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Album> Releases { get; set; }
    }
}