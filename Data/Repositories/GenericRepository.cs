using Data.Interfaces;
using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class 
    {
        string _errorMessage = string.Empty;

        DbSet<T> _entities;
        IDataContext _context;

        public GenericRepository(IDataContext context)
        {
            _context = context;
        }

        protected virtual DbSet<T> Entities
        {
            get { return _entities ?? (_entities = _context.Set<T>()); }
        }

        /// <summary>
        /// Get all queries with including 
        /// </summary>
        /// <param name="includeProperties">Represents a string paramter</param>
        /// <returns>IEnumerable data</returns>
        protected IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties)
        {
            foreach (var includeProperty in includeProperties.Split
               (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public async Task<IEnumerable<T>> SearchAsync(Func<IQueryable<T>, IQueryable<T>> transform)
        {
            var query = Entities.AsQueryable();
            query = transform(query);
            return await query.ToListAsync();
        }

        public async Task<SearchResult<T>> SearchAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int page, int size)
        {
            var query = Entities.AsQueryable<T>();
            query = query.Where(filter);
            query = sort(query);
            var totalItems = query.Count();
            query = query.Skip(size * (page - 1)).Take(size);
            var items = await query.ToListAsync();
            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.Page = page;
            pagingInfo.Size = size;
            pagingInfo.TotalItems = totalItems;
            var pageCount = totalItems / size;
            if (totalItems % size != 0)
            {
                pageCount = pageCount + 1;
            }
            if (page == pageCount)
                pagingInfo.HasNextPage = false;
            else
                pagingInfo.HasNextPage = true;

            SearchResult<T> searchResult = new SearchResult<T>();
            searchResult.PagingInfo = pagingInfo;
            searchResult.Items = items;
            return searchResult;
        }
        public async Task<SearchResult<T>> SearchAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> sort, int page, int size, string includeProperties)
        {
            var query = Entities.AsQueryable<T>();
            query = query.Where(filter);
            query = sort(query);
            var totalItems = query.Count();
            query = query.Skip(size * (page - 1)).Take(size);
            query = IncludeProperties(query, includeProperties);
            var items = await query.ToListAsync();
            PagingInfo pagingInfo = new PagingInfo();
            pagingInfo.Page = page;
            pagingInfo.Size = size;
            pagingInfo.TotalItems = totalItems;
            var pageCount = totalItems / size;
            if (totalItems % size != 0)
            {
                pageCount = pageCount + 1;
            }
            if (page >= pageCount)
                pagingInfo.HasNextPage = false;
            else
                pagingInfo.HasNextPage = true;

            SearchResult<T> searchResult = new SearchResult<T>();
            searchResult.PagingInfo = pagingInfo;
            searchResult.Items = items;
            return searchResult;
        }
    }
}
