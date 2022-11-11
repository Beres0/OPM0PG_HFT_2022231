using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace OPM0PG_HFT_2022231.Models.Support.Reflection
{
    public static class GenericFactory
    {
        public static Dictionary<Type, TFunc> CreateFactories<TFunc>(Type generic, Type[] factoryTypes)
         where TFunc : Delegate
        {
            var factories = new Dictionary<Type, TFunc>();

            var funcType = typeof(TFunc);
            var parameterTypes = funcType.GenericTypeArguments.Take(funcType.GenericTypeArguments.Length - 1).ToArray();
            var paramsExp = new ParameterExpression[parameterTypes.Length];

            for (int i = 0; i < paramsExp.Length; i++)
            {
                paramsExp[i] = Expression.Parameter(parameterTypes[i]);
            }

            foreach (var item in factoryTypes)
            {
                ConstructorInfo constructor = generic.GetGenericTypeDefinition()
                                            .MakeGenericType(item)
                                            .GetConstructor(parameterTypes);
                factories.Add(item, Expression.Lambda<TFunc>(Expression.New(constructor, paramsExp), paramsExp).Compile());
            }
            return factories;
        }

        public static Dictionary<Type, TFunc> CreateFactoriesFromEntities<TFunc>(Type generic)
         where TFunc : Delegate
        {
            return CreateFactories<TFunc>(generic, ReflectionCollector.CollectEntityTypes());
        }
    }
}