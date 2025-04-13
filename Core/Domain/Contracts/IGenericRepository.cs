using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity , T>  where TEntity : BaseEntity<T>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false);
        Task<TEntity?> GetAsync(T id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity); 

    }
}
