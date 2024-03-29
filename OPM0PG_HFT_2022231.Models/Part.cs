﻿using OPM0PG_HFT_2022231.Models.Utility.Reflection;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class Part : IEntity
    {
        public Part()
        {
            InversePropertiesSetter<Part>.SetInverseProperties(this);
        }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        public int AlbumId { get; set; }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual ICollection<Track> Tracks { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}