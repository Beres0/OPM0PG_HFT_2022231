using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Track : IEntity
    {
        public TimeSpan? Duration { get; set; }

        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [System.Text.Json.Serialization.JsonIgnore, JsonIgnore, XmlIgnore]
        public virtual Part Part { get; set; }

        [Range(0, int.MaxValue)]
        public int PartId { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        [System.Text.Json.Serialization.JsonIgnore, StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}