﻿using System;
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
            if (spec.OrderBy != null)
            {
                query = query.OrderBy(spec.OrderBy);
            }   
            else if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending(spec.OrderByDescending);
            }

            if (spec.IsPagination)
            {
                query = query.Skip(spec.Skip).Take(spec.Take);

            }
            query = spec.IncludeExpressions.Aggregate(query, (currentquery, includeExpression) => currentquery.Include(includeExpression));
          

            return query;
        }
    }
}
