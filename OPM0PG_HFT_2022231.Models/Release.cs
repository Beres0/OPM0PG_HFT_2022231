using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class Release : IEntity
    {
        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        public int AlbumId { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        public string Country { get; set; }

        public int Id { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        public string Publisher { get; set; }

        [Range(ColumnTypeConstants.MinYear, ColumnTypeConstants.MaxYear)]
        public int? ReleaseYear { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}