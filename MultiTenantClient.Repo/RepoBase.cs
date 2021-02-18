using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiTenantClient.Entities;
using MultiTenantClient.Repo.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo
{
    public class RepoBase<TEntity> : IRepoBase<TEntity> where TEntity : BaseEntity
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private MultiTenantClientContext _context;
        public RepoBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = unitOfWork.GetDbContext();
        }



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

        public TDto GetByIdToDto<TDto>(Guid key) where TDto : class, new()
        {
            return _mapper.Map<TDto>(_context.Set<TEntity>().FirstOrDefault(x => x.Id == key));
        }

        public async Task<TDto> GetByIdToDtoAsync<TDto>(Guid key) where TDto : class, new()
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
    }
}
