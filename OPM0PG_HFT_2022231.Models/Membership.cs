﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Membership : IEntity
    {
        public bool Active { get; set; }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual Artist Band { get; set; }

        [Range(0, int.MaxValue)]
        public int BandId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore,JsonIgnore, XmlIgnore]
        public virtual Artist Member { get; set; }

        [Range(0, int.MaxValue)]
        public int MemberId { get; set; }

        public object[] GetId() => new object[] { BandId, MemberId };
    }
}