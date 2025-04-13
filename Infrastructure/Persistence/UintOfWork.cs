using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Persistence.Data;
using Persistence.Repositories;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly StoreDbContext _context;
        private readonly Dictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }
        public IGenericRepository<TEntity, T> GetRepository<TEntity, T>() where TEntity : BaseEntity<T>
        {
            //return new GenericRepository<TEntity, T>(_context); 
            var type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity, T>(_context);
                _repositories.Add(type, repository);
                //return repository;
            }

            return (IGenericRepository<TEntity, T>)_repositories[type];

        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
