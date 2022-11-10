using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class Track : IEntity
    {
        public TimeSpan? Duration { get; set; }

        public int Id { get; set; }

        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Part Part { get; set; }

        public int PartId { get; set; }

        [Range(1, int.MaxValue)]
        public int Position { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}