using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OPM0PG_HFT_2022231.Logic.Internals
{
    internal static class Validator
    {
        public static void ValidateForeignKey<TEntity>(object id,
                                          IRepository<TEntity> repository,
                                          [CallerArgumentExpression("id")] string argName = null)
            where TEntity : class, IEntity
        {
            ValidateForeignKey(new object[] { id }, repository, argName);
        }


        public static void ValidateForeignKey<TEntity>(object[] id,
                                          IRepository<TEntity> repository,
                                          [CallerArgumentExpression("id")] string argName = null) where TEntity : class, IEntity
        {
            try
            {
                repository.Read(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new ArgumentException($"The given foreign key '{argName}' not found! ", ex);
            }
        }

        public static void ValidatePositiveNumber(int number, [CallerArgumentExpression("number")] string argName = null)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException($"The '{argName}' must be positive! Actual value: {number}");
            }
        }

        public static void ValidateRequiredText(string text,
                                           [CallerArgumentExpression("text")] string argName = null)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(null, $"The '{argName}' is missing!");
            ValidateText(text, argName);
        }

        public static void ValidateText(string text,
                                    [CallerArgumentExpression("text")] string argName = null)
        {
            if (text is not null && text.Length > ColumnTypeConstants.MaxTextLength)
            {
                throw new ArgumentException($" Length of '{argName}' is too long! Actual length:{text.Length} Max length:{ColumnTypeConstants.MaxTextLength}");
            }
        }

        public static void ValidateYear(int? year,
                                    [CallerArgumentExpression("year")] string argName = null)
        {
            if (year.HasValue && (year < ColumnTypeConstants.MinYear || year > ColumnTypeConstants.MaxYear))
            {
                throw new ArgumentOutOfRangeException
                    (null, $"The '{argName}' must be between {ColumnTypeConstants.MinYear} and {ColumnTypeConstants.MaxYear}! Actual value: {year}");
            }
        }
    }
}
