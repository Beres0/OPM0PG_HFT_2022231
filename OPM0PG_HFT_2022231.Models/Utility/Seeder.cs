using OPM0PG_HFT_2022231.Models.Utility.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Utility
{
    public static class Seeder
    {
        public static string DefaultDirectory { get; set; } = "Seeds";

        public static List<TEntity> ReadSeed<TEntity>()
            where TEntity : class, IEntity
        {
            return ModelXmlSerializer<List<TEntity>>.Deserialize($"{DefaultDirectory}/{typeof(TEntity).Name}Seed.xml");
        }

        public static List<TEntity> ReadSeed<TEntity>(string path)
            where TEntity : class, IEntity
        {
            return ModelXmlSerializer<List<TEntity>>.Deserialize(path);
        }
    }
}