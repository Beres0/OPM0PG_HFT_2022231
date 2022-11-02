using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace OPM0PG_HFT_2022231.Models.Support
{
    public static class Validator<T>
    {
        private static readonly Dictionary<string, Dictionary<Type, ValidationAttribute>> validators = CollectAttributes();

        public static bool IsValid<TValidation, TValue>(TValue value, [CallerArgumentExpression("value")] string propName = null)
            where TValidation : ValidationAttribute
        {
            return GetValidator<TValidation>(propName).IsValid(value);
        }

        public static void Throws<TValue>(TValue value, ValidationAttribute attribute, [CallerArgumentExpression("value")] string propName = null)
        {
            if (!attribute.IsValid(value))
            {
                if (attribute is RangeAttribute range)
                {
                    throw new ArgumentOutOfRangeException($"The '{propName}' was out of range. Argument must be between {range.Minimum} and {range.Maximum}! Actual value: {value}");
                }
                else if (attribute is RequiredAttribute required)
                {
                    throw new ArgumentNullException($"The '{propName}' is missing!");
                }
                else if (attribute is StringLengthAttribute stringLength)
                {
                    throw new ArgumentException($"The lenght of '{propName}' is not valid! Must be between {stringLength.MinimumLength} and {stringLength.MaximumLength}! Actual length: {value}");
                }
            }
        }

        public static void Throws<TValue>(TValue value, [CallerArgumentExpression("value")] string propName = null, params Type[] filters)
        {
            foreach (var filter in filters)
            {
                if (validators[Normalize(propName)].TryGetValue(filter, out ValidationAttribute attribute))
                {
                    Throws(value, attribute, propName);
                }
            }
        }

        public static void Validate<TValue>(TValue value, [CallerArgumentExpression("value")] string propName = null)
        {
            foreach (var attribute in validators[Normalize(propName)])
            {
                Throws(value, attribute.Value, propName);
            }
        }

        private static Dictionary<string, Dictionary<Type, ValidationAttribute>> CollectAttributes()
        {
            var validators = new Dictionary<string, Dictionary<Type, ValidationAttribute>>();

            foreach (var prop in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead))
            {
                string propName = Normalize(prop.Name);
                validators.TryAdd(propName, new Dictionary<Type, ValidationAttribute>());
                foreach (var attr in prop.GetCustomAttributes<ValidationAttribute>())
                {
                    validators[propName].TryAdd(attr.GetType(), attr);
                }
            }
            return validators;
        }

        private static ValidationAttribute GetValidator<TValidation>(string propName)
            where TValidation : ValidationAttribute
        {
            return validators[Normalize(propName)][typeof(TValidation)];
        }

        private static string Normalize(string propName)
        {
            int lastIndex = propName.LastIndexOf('.');
            if (lastIndex == -1)
            {
                return propName.ToLower();
            }
            else return propName.Substring(lastIndex + 1).ToLower();
        }
    }
}