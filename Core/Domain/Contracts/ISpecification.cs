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

        Expression<Func<TEntity, object>>? OrderBy { get; set; }
        Expression<Func<TEntity, object>>? OrderByDescending { get; set; }

        int Take { get; set; }
        int Skip { get; set; }

        bool IsPagination { get; set; }

        //Expression<Func<TEntity, object>>? GroupBy { get; set; }
        //Expression<Func<TEntity, object>>? Select { get; set; }
        //int Count { get; set; }
        ////bool IsReadOnly { get; set; }


    }
}
