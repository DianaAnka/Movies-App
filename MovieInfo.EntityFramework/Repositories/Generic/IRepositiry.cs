using MovieInfo.Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieInfo.EntityFramework.Repositories.Generic
{
    public interface IRepositiry<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
        Task Delete(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id, params Expression<Func<T, object>>[] includes);
        Task<T> Update(T entity);
    }
}