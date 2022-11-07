using Newtonsoft.Json;
using OPM0PG_HFT_2022231.Models.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Artist : IEntity
    {
        public Artist()
        {
            InversePropertiesSetter<Artist>.SetInverseProperties(this);
        }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual ICollection<Membership> Bands { get; set; }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual ICollection<Contribution> ContributedAlbums { get; set; }

        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual ICollection<Membership> Members { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}