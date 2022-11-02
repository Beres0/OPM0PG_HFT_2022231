using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Models.Support;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace OPM0PG_HFT_2022231.Client.Readers
{
    public class EntityReader<TEntity> : ConsoleTypeReader
        where TEntity : class, IEntity, new()
    {
        private static Dictionary<Type, Func<string, object>> converters = new Dictionary<Type, Func<string, object>>()
        {
            {typeof(TimeSpan),(s)=>TimeSpan.ParseExact(s,@"hh\:mm\:ss",CultureInfo.CurrentCulture) },
            {typeof(TimeSpan?),(s)=>TimeSpan.TryParseExact(s,@"hh\:mm\:ss",CultureInfo.CurrentCulture, out var result)?result:null},
        };

        public EntityReader() : base(typeof(TEntity))
        { }

        protected override object ReadMethod()
        {
            Console.WriteLine(typeof(TEntity).Name + ":");
            TEntity entity = new TEntity();
            foreach (var item in EntityPropertyCollector<TEntity>.CollectProperties())
            {
                bool succes = false;
                while (!succes)
                {
                    try
                    {
                        Console.Write($"\t{item.Name}({item.PropertyType.Name}): ");
                        object input;
                        if (converters.TryGetValue(item.PropertyType, out var converter))
                        {
                            input = converter(Console.ReadLine());
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