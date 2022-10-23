using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Artist : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Member> Members { get; set; }
        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Member> Bands { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Contributor> ContributedAlbums { get; set; }
    }
}