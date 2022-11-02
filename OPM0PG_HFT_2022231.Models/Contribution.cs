﻿using OPM0PG_HFT_2022231.Models.Support;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Contribution : IEntity
    {
        public Contribution()
        {
            InversePropertySetter<Contribution>.SetCollections(this);
        }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [Range(0, int.MaxValue)]
        public int AlbumId { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Artist Artist { get; set; }

        [Range(0, int.MaxValue)]
        public int ArtistId { get; set; }

        public object[] GetId() => new object[] { AlbumId, ArtistId };
    }
}