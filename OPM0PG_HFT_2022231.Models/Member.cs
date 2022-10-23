using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Member : IEntity<object>
    {
        [Key, Column(Order = 0), ForeignKey(nameof(Band))]
        public int BandId { get; set; }

        [Key, Column(Order = 1), ForeignKey(nameof(Artist))]
        public int ArtistId { get; set; }
        public bool Active { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Artist { get; set; }

        [JsonIgnore, XmlIgnore, NotMapped]
        object IEntity<object>.Id => new { BandId, ArtistId };
    }
}