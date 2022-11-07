using Newtonsoft.Json;
using OPM0PG_HFT_2022231.Models.Support;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Part : IEntity
    {
        public Part()
        {
            InversePropertiesSetter<Part>.SetInverseProperties(this);
        }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [Range(0, int.MaxValue)]
        public int AlbumId { get; set; }

        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}