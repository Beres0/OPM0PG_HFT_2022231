using System.ComponentModel.DataAnnotations;

namespace OPM0PG_HFT_2022231.Models
{
    public enum MediaType
    {
        Picture, Youtube, Bandcamp
    }

    public abstract class Media : IEntity
    {
        public int Id { get; set; }
        public bool Main { get; set; }
        public MediaType MediaType { get; set; }

        [Url]
        public string Uri { get; set; }

        public object[] GetId() => new object[] { Id };
    }
}