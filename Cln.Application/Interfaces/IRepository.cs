using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cln.Application.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        void Delete(TEntity entityToDelete);

        Task<IEnumerable<TResult>> Get<TResult>();

        Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> filter,
                                                string includeProperties);

        Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> filter,
                                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                string includeProperties,
                                                int skip,
                                                int take);

        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter);

        Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter,
                                       string includeProperties);

        Task<TEntity> GetById(TKey id);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        IQueryable<TEntity> GetQuery();
    }
}