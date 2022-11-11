using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Utility.Reflection;
using OPM0PG_HFT_2022231.Models.Utility.Serialization;
using System;

namespace OPM0PG_HFT_2022231.Client.Readers
{
    public class EntityReader<TEntity> : ConsoleTypeReader
        where TEntity : class, IEntity, new()
    {
        public EntityReader() : base(typeof(TEntity))
        { }

        protected override object ReadMethod()
        {
            Console.WriteLine(typeof(TEntity).Name + ":");
            TEntity entity = new TEntity();
            foreach (var item in ReflectionCollector.CollectNonVirtualProperties<TEntity>())
            {
                bool succes = false;
                while (!succes)
                {
                    try
                    {
                        Console.Write($"\t{item.Name}({item.PropertyType.Name}): ");
                        object input;
                        if (ModelJsonSerializer.TryGetConverter(item.PropertyType, out var converter))
                        {
                            input = converter.Convert(Console.ReadLine());
                        }
                        else
                        {
                            input = Convert.ChangeType(Console.ReadLine(), item.PropertyType);
                        }
                        item.SetValue(entity, input);
                        succes = true;
                    }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            };
            return entity;
        }
    }
}