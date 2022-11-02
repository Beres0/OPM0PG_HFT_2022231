﻿using OPM0PG_HFT_2022231.Models.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Album : IEntity
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Range(ColumnTypeConstants.MinYear, ColumnTypeConstants.MaxYear)]
        public int Year { get; set; }

        public Album()
        {
            InversePropertySetter<Album>.SetCollections(this);
        }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Contribution> Contributions { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<AlbumGenre> Genres { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Part> Parts { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual ICollection<Release> Releases { get; set; }

        public object[] GetId() => new object[] { Id };

     
    }
}