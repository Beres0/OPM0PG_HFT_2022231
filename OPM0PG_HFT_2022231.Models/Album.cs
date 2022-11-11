using OPM0PG_HFT_2022231.Models.Utility.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class Album : IEntity
    {
        public Album()
        {
            InversePropertiesSetter<Album>.SetInverseProperties(this);
        }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Contribution> Contributions { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<AlbumGenre> Genres { get; set; }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<AlbumMedia> Media { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Part> Parts { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Release> Releases { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Range(ColumnTypeConstants.MinYear, ColumnTypeConstants.MaxYear)]
        public int Year { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}