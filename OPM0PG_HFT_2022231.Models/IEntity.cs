using System;

namespace OPM0PG_HFT_2022231.Models
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
