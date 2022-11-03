using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Release : IEntity
    {
        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [Range(0, int.MaxValue)]
        public int AlbumId { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        public string Country { get; set; }

        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        public string Publisher { get; set; }

        [Range(ColumnTypeConstants.MinYear, ColumnTypeConstants.MaxYear)]
        public int? ReleaseYear { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}