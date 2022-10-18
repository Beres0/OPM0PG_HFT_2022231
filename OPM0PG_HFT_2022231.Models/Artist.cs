using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM0PG_HFT_2022231.Models
{
    public class Artist:IEntity<int>
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey(nameof(Members))]
        public int? BandId { get; set; }
        public virtual ICollection<Artist> Members { get; set; }
    }
}
