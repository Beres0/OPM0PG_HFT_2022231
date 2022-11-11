using OPM0PG_HFT_2022231.Models.Support.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Seeding
{
    public static class Seeder
    {
        private static readonly string BuildDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string DefaultDirectory { get; set; } = "Seeding";

        public static List<TEntity> ReadSeed<TEntity>()
            where TEntity : class, IEntity
        {
            return ModelXmlSerializer<List<TEntity>>.Deserialize($"{BuildDirectory}/{DefaultDirectory}/Seeds/{typeof(TEntity).Name}Seed.xml");
        }

        public static List<TEntity> ReadSeed<TEntity>(string path)
            where TEntity : class, IEntity
        {
            return ModelXmlSerializer<List<TEntity>>.Deserialize(path);
        }
    }
}