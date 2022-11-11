using OPM0PG_HFT_2022231.Models.Utility.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class Artist : IEntity
    {
        public Artist()
        {
            InversePropertiesSetter<Artist>.SetInverseProperties(this);
        }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Membership> Bands { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Contribution> ContributedAlbums { get; set; }

        public string Description { get; set; }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<ArtistMedia> Media { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Membership> Members { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}