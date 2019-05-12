using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Cln.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cln.Infrastructure
{
    /// <summary>
    /// Base class for implementing a repository for entity framework.
    /// </summary>
    /// <typeparam name="TEntity">The EF entity the repository applies to.</typeparam>
    /// <typeparam name="TKey">The type of the entities primary key.</typeparam>
    public class DbContextRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;
        private readonly DbSet<TEntity> _dbSet;

        public DbContextRepository(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Marks an entity for deletion. Saving changes is done using the unit of work pattern.
        /// </summary>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                _dbSet.Attach(entityToDelete);
            }

            _dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// For OData testing. Not normally used.
        /// </summary>
        public virtual IQueryable<TEntity> GetQuery()
        {
            IQueryable<TEntity> query = CreateQuery(null, null, null, 0, 0);

            return query;
        }

        /// <summary>
        /// Gets TEntity projected to non-updatable TResult using Automapper. TEntity and TResult need an automapper configuration.
        /// </summary>
        public virtual async Task<IEnumerable<TResult>> Get<TResult>()
        {
            return await Get<TResult>(null, null, null, 0, 0);
        }

        /// <summary>
        /// Gets TEntity projected to non-updatable TResult using Automapper. TEntity and TResult need an automapper configuration.
        /// </summary>
        public virtual async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> filter)
        {
            return await Get<TResult>(filter, null, null, 0, 0);
        }

        /// <summary>
        /// Gets TEntity projected to non-updatable TResult using Automapper. TEntity and TResult need an automapper configuration.
        /// </summary>
        public virtual async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> filter,
                                                                     string includeProperties)
        {
            return await Get<TResult>(filter, null, includeProperties, 0, 0);
        }

        /// <summary>
        /// Gets TEntity projected to non-updatable TResult using Automapper. TEntity and TResult need an automapper configuration.
        /// </summary>
        public virtual async Task<IEnumerable<TResult>> Get<TResult>(Expression<Func<TEntity, bool>> filter,
                                                                     Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                                                     string includeProperties,
                                                                     int skip,
                                                                     int take)
        {
            IQueryable<TEntity> query = CreateQuery(filter, orderBy, includeProperties, skip, take);

            return await query.ProjectTo<TResult>(_mapper.ConfigurationProvider).ToListAsync();
        }

        /// <summary>
        /// Gets entities of type TEntity. These entities are change tracked and can be used for updates.
        /// </summary>
        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter)
        {
            return await Get(filter, null);
        }

        /// <summary>
        /// Gets updatable entities.
        /// </summary>
        public async Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            IQueryable<TEntity> query = CreateQuery(filter, null, includeProperties, 0, 0);

            return await query.ToListAsync();
        }

        /// <summary>
        /// Gets updatable entities.
        /// </summary>
        public virtual async Task<TEntity> GetById(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Inserts a new entity into the change tracker. Saving changes is done using the unit of work pattern.
        /// </summary>
        public virtual void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <summary>
        /// Updates a new entity by adding it to the change tracker. If you use one of the Get methods to query an 
        /// entity it will be change tracked already and you will not have to call this method. Simply update the entity 
        /// and call SaveChanges. Saving changes is done using the unit of work pattern.
        /// </summary>
        public virtual void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        private IQueryable<TEntity> CreateQuery(Expression<Func<TEntity, bool>> filter,
                                               Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                                               string includeProperties,
                                               int skip,
                                               int take)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrWhiteSpace(includeProperties))
            {
                query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            query = orderBy != null ? orderBy(query) : query;
            query = skip > 0 ? query.Skip(skip) : query;
            query = take > 0 ? query.Take(take) : query;

            return query;
        }
    }
}