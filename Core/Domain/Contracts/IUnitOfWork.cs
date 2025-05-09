﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, T> GetRepository<TEntity, T>() where TEntity : BaseEntity<T>;
    }
}
