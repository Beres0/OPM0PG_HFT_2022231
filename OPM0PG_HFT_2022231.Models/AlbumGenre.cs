using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class AlbumGenre : IEntity
    {
        public int AlbumId { get; set; }
        public string Genre { get; set; }

        [JsonIgnore, XmlIgnore]
        public virtual Album Album { get; set; }

        object[] IEntity.GetId() => new object[] {AlbumId,Genre };
    }
}