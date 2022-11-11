using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPM0PG_HFT_2022231.Models
{
    public enum MediaType
    {
        Picture, Youtube, Bandcamp
    }

    public abstract class Media : IEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool Main { get; set; }
        public MediaType MediaType { get; set; }

        [Url]
        public string Uri { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}