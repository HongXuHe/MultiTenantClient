using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultiTenantClient.Entities;
using MultiTenantClient.Repo.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo
{
    public class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : BaseEntity
    {
        #region ctor and fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RepoBase<TEntity>> _logger;
        private MultiTenantClientContext _context;
        public RepoBase(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RepoBase<TEntity>> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _context = unitOfWork.GetDbContext();
        }
        #endregion

        #region Search

        public IQueryable<TEntity> NoTrackEntities
        {
            get
            {
                return _context.Set<TEntity>().AsNoTracking();
            }
        }

        public IQueryable<TEntity> TrackEntities
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
        }



        public IQueryable<TEntity> ExecuteSql(string sql, params object[] parameters)
        {
            return _context.Set<TEntity>().FromSqlRaw(sql, parameters);
        }

        public TEntity GetById(Guid key)
        {
            return _context.Set<TEntity>().FirstOrDefault(x => x.Id == key);
        }

        public async Task<TEntity> GetByIdAsync(Guid key)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == key);
        }

        public virtual TDto GetByIdToDto<TDto>(Guid key) where TDto : class, new()
        {
            return _mapper.Map<TDto>(_context.Set<TEntity>().FirstOrDefault(x => x.Id == key));
        }

        public virtual async Task<TDto> GetByIdToDtoAsync<TDto>(Guid key) where TDto : class, new()
        {
            return _mapper.Map<TDto>(await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == key));
        }

        public IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> whereLambda)
        {
            return _context.Set<TEntity>().Where(whereLambda);
        }

        public IQueryable<TEntity> GetEntities<s>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, s>> orderByLambda, bool IsAcs = true)
        {
            var list = _context.Set<TEntity>().Where(whereLambda);
            if (IsAcs)
            {
                list = list.OrderBy(orderByLambda);
            }
            else
            {
                list = list.OrderByDescending(orderByLambda);
            }
            return list;
        }

        public IQueryable<TEntity> GetEntities<s>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, s>> orderByLambda, bool IsAcs = true, int pageIndex = 1, int pageSize = 10)
        {
            var list = _context.Set<TEntity>().Where(whereLambda);
            if (IsAcs)
            {
                list = list.OrderBy(orderByLambda);
            }
            else
            {
                list = list.OrderByDescending(orderByLambda);
            }
            var skipCount = (pageIndex - 1) * pageSize > 0 ? ((pageIndex - 1) * pageSize) : 1;
            return list.Skip(skipCount).Take(pageSize);
        }

        #endregion

        #region Insert

        /// <summary>
        /// insert entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Insert(params TEntity[] entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            _logger.LogInformation("entities been added", entities);
            return _context.SaveChanges();
        }

        /// <summary>
        /// insert entities async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            _logger.LogInformation("entitie been added", entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// insert entities async
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity[] entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
            _logger.LogInformation("entities been added", entities);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// insert async using dto. can add delegate before/after insert
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="dto"></param>
        /// <param name="checkFunc"></param>
        /// <param name="insertFunc"></param>
        /// <param name="completeFunc"></param>
        /// <returns></returns>
        public async Task<int> InsertAsync<TDto>(TDto dto, Func<TDto, Task> checkFunc = null, Func<TDto, TEntity, Task<TEntity>> insertFunc = null, Func<TEntity, TDto> completeFunc = null)
        {
            if (checkFunc != null)
            {
                await checkFunc(dto);
            }
            var entity = _mapper.Map<TEntity>(dto);
            if (insertFunc != null)
            {
                entity = await insertFunc(dto, entity);
            }
            await _context.AddAsync(entity);
            if (completeFunc != null)
            {
                dto = completeFunc(entity);
            }
            _logger.LogInformation("entity been added", entity);
            return await _context.SaveChangesAsync();
        }
        #endregion

        #region Update

        /// <summary>
        /// update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _logger.LogInformation("entity been updated", entity);
            return _context.SaveChanges();
        }

        /// <summary>
        /// update entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            _logger.LogInformation("entity been updated", entity);
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// update entities at one time
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity[] entities)
        {
            _context.Set<TEntity>().UpdateRange(entities);
            _logger.LogInformation("entities been updated", entities);
            return await _context.SaveChangesAsync();
        }
        #endregion
        #region Delete

        /// <summary>
        /// delete entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public int Delete(params TEntity[] entities)
        {
            if (entities != null && entities.Length > 0)
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    entities[i].SoftDelete = true;
                }
                _logger.LogInformation("entities been delete", entities);
                return _context.SaveChanges();
            }

            return -1;
        }

        /// <summary>
        /// delete entity, return negative if false
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                entity.SoftDelete = true;
                _logger.LogInformation("entity been delete", entity);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        /// <summary>
        /// delete entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            if (entity != null)
            {
                entity.SoftDelete = true;
                _logger.LogInformation("entity been delete", entity);
                return await _context.SaveChangesAsync();
            }
            return -1;
        }

        /// <summary>
        /// delete entities by condition
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> whereLambda)
        {
            var entitiesToDelete = await _context.Set<TEntity>().Where(whereLambda).ToArrayAsync();
            return Delete(entitiesToDelete);
        }

        public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> whereLambda)
        {
            return await _context.Set<TEntity>().AnyAsync(whereLambda);
        }
        #endregion

    }
}
