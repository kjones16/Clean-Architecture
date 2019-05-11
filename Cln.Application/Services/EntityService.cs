using Cln.Application.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Cln.Application.Services
{
    /// <summary>
    /// WORK IN PROGRESS. PLEASE IGNORE
    /// </summary>
    public class EntityService<TEntity, TKey>
    {
        private IRepository<TEntity, TKey> _repository;
        private IUnitOfWork _unitOfWork;

        public EntityService(IRepository<TEntity, TKey> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async virtual Task<TEntity> Get(TKey id)
        {
            return await _repository.GetById(id);
        }

        public  virtual IQueryable<TEntity> Get()
        {
            return _repository.GetQuery();
        }

        public async virtual Task<TEntity> Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _repository.Update(entity);
            await _unitOfWork.SaveChanges();

            return entity;
        }

        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _repository.Insert(entity);
            await _unitOfWork.SaveChanges();

            return entity;
        }

        public async virtual Task Delete(TKey id)
        {
            var entity = await _repository.GetById(id);

            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            _repository.Delete(entity);
            await _unitOfWork.SaveChanges();
        }
    }
}
