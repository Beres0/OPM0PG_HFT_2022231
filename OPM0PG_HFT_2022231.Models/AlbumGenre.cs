using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using NativeJson = System.Text.Json;

namespace OPM0PG_HFT_2022231.Models
{
    public class AlbumGenre : IEntity
    {
        [NativeJson.Serialization.JsonIgnore, Newtonsoft.Json.JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        public int AlbumId { get; set; }

        [StringLength(ColumnTypeConstants.MaxTextLength)]
        [Required(AllowEmptyStrings = false)]
        public string Genre { get; set; }

        public object[] GetId() => new object[] { AlbumId, Genre };
    }
}