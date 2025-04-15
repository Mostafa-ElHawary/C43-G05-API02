using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface ISpecification<TEntity , T> where TEntity : BaseEntity<T>
    {

        Expression<Func<TEntity, bool>>? Criteria { get; set; }
        List<Expression<Func<TEntity, object>>> IncludeExpressions { get; set; }

        //Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; }
        //Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderByDescending { get; }

        //int? Take { get; }
        //int? Skip { get; }


    }
}
