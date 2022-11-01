using OPM0PG_HFT_2022231.Models.Support;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Release : IEntity
    {
        public Release()
        {
            InversePropertySetter<Release>.SetCollections(this);
        }

        [Range(0, int.MaxValue)]
        public int Id { get; set; }

        [Range(0, int.MaxValue)]
        public int AlbumId { get; set; }

        [Range(ColumnTypeConstants.MinYear, ColumnTypeConstants.MaxYear)]
        public int? ReleaseYear { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        public string Publisher { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        public string Country { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        object[] IEntity.GetId() => new object[] { Id };
    }
}