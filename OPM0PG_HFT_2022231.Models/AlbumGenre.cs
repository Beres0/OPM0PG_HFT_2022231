using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class AlbumGenre : IEntity
    {
        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        [Range(0, int.MaxValue)]
        public int AlbumId { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Genre { get; set; }

        public object[] GetId() => new object[] { AlbumId, Genre };
    }
}