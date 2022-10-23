using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OPM0PG_HFT_2022231.Models
{
    public class Release
    {
        public int Id { get; set; }
        public int AlbumId { get; set; }
        public int? ReleaseYear { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }

        [JsonIgnore,XmlIgnore]
        public virtual Album Album { get; set; }
    }
}
