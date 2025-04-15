﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;

namespace Services.Specifications
{
    public class BaseSpecification<TEntity, T> : ISpecification<TEntity,T>
        where TEntity : BaseEntity<T>
    {
        public Expression<Func<TEntity, bool>>? Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get ; set; } = new List<Expression<Func<TEntity, object>>>();

        public BaseSpecification(Expression<Func<TEntity,bool>>? expression)
        {
            Criteria = expression;
        }

        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExpressions.Add(expression);
        }

    }
}
