using OPM0PG_HFT_2022231.Models;
using OPM0PG_HFT_2022231.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OPM0PG_HFT_2022231.Logic.Implementations
{
   
    public abstract class BaseLogic
    {
        protected readonly IMusicRepository repository;
        
        protected BaseLogic(IMusicRepository repository)
        {
            this.repository=repository;
        }
       
        protected void ValidateForeignKey<TEntity>(object id,
                                          IRepository<TEntity> repository,
                                          [CallerArgumentExpression("id")] string argName = null) 
            where TEntity : class, IEntity
        {
            ValidateForeignKey(new object[] { id }, repository, argName);
        }

        protected void ValidateUniqueness<TEntity>(TEntity entity, IEnumerable<TEntity> group,Func<TEntity,object> selector, [CallerArgumentExpression("entity")] string argName = null)
        {
            object selection = selector(entity);
            if(group.Any(g => selector(g).Equals(selection)))
            {
                throw new ArgumentException($"The given '{argName}' entity is not unique! Selected properties: {selection}");
            };
        }

        protected void ValidateForeignKey<TEntity>(object[] id, 
                                          IRepository<TEntity> repository,
                                          [CallerArgumentExpression("id")] string argName=null) where TEntity:class,IEntity
        {
            try
            {
                repository.Read(id);
            }
            catch(KeyNotFoundException ex)
            {
                throw new ArgumentException($"The given foreign key '{argName}' not found! ", ex);
            }
        }

        protected void ValidatePositiveNumber(int number, [CallerArgumentExpression("number")] string argName = null)
        {
            if (number < 0)
            {
                throw new ArgumentOutOfRangeException($"The '{argName}' must be positive! Actual value: {number}");
            }
        }
        protected void ValidateRequiredText(string text,
                                           [CallerArgumentExpression("text")] string argName = null)
        {
            if (string.IsNullOrWhiteSpace(text)) throw new ArgumentNullException(null,$"The '{argName}' is missing!");
            ValidateText(text, argName);
        }
        protected void ValidateText(string text,
                                    [CallerArgumentExpression("text")] string argName = null)
        {
            if(text is not null && text.Length > ColumnTypeConstants.MaxTextLength)
            {
                throw new ArgumentException($" Length of '{argName}' is too long! Actual length:{text.Length} Max length:{ColumnTypeConstants.MaxTextLength}");
            }   
        }
        protected void ValidateYear(int? year,
                                    [CallerArgumentExpression("year")] string argName = null)
        {
            if (year.HasValue && (year < ColumnTypeConstants.MinYear || year > ColumnTypeConstants.MaxYear))
            {
                throw new ArgumentOutOfRangeException
                    (null,$"The '{argName}' must be between {ColumnTypeConstants.MinYear} and {ColumnTypeConstants.MaxYear}! Actual value: {year}");
            }
        }

    }
}
