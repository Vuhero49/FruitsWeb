﻿using eCommerce.Models;
using System.Linq.Expressions;

namespace eCommerce.Data.Base
{
    public interface IEntityBaseReponsitory<T> where T : class, IEntityBase, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        Task UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);
    }
}
