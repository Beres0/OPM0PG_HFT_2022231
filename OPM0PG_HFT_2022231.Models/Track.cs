using OPM0PG_HFT_2022231.Models.Support;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Track : IEntity
    {
        public Track()
        {
            InversePropertySetter<Track>.SetCollections(this);
        }

        [Range(0, int.MaxValue)]
        public int PartId { get; set; }

        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        public TimeSpan? Duration { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Part Part { get; set; }

        object[] IEntity.GetId() => new object[] { Id };
    }
}