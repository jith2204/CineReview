using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<SearchResult<T>> SearchAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int page, int size);
        Task<IEnumerable<T>> SearchAsync(Func<IQueryable<T>, IQueryable<T>> transform);
        Task<SearchResult<T>> SearchAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int page, int size, string includeProperties);
    }
}
