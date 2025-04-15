using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GetQuery<TEntity, T>(
            IQueryable<TEntity> inputQuery,
            ISpecification<TEntity,T> spec)
            where TEntity : BaseEntity<T>
        {
            var query = inputQuery;

            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            query = spec.IncludeExpressions.Aggregate(query, (currentquery, includeExpression) => currentquery.Include(includeExpression));
          

            return query;
        }
    }
}
