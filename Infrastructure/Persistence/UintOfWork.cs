using System;
using System.Collections.Concurrent;
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
        //private readonly Dictionary<string, object> _repositories;
        private readonly ConcurrentDictionary<string, object> _repositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            //_repositories = new Dictionary<string, object>();
            _repositories = new ConcurrentDictionary<string, object>();

        }
        public IGenericRepository<TEntity, T> GetRepository<TEntity, T>() where TEntity : BaseEntity<T>
        => (IGenericRepository<TEntity, T>) _repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, T>(_context));

        
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
