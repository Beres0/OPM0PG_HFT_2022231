using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Release
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(Album))]
        public int AlbumId { get; set; }
        [Range(1800, 2100)]
        public int? ReleaseYear { get; set; }
        [StringLength(255), Required]
        public string Publisher { get; set; }
        [StringLength(255), Required]
        public string Country { get; set; }

        [JsonIgnore,XmlIgnore]
        public virtual Album Album { get; set; }
    }
}
