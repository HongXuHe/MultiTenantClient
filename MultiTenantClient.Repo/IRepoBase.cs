﻿using MultiTenantClient.Entities;
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
    public interface IRepoBase<TEntity> where TEntity:BaseEntity
    {
        /// <summary>
        /// get unitofwork
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        #region Select
        /// <summary>
        /// no track entities
        /// </summary>
        IQueryable<TEntity> NoTrackEntities { get; }

        /// <summary>
        /// track entities
        /// </summary>
        IQueryable<TEntity> TrackEntities { get; }

        /// <summary>
        /// get by id
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TEntity GetById(Guid key);

        /// <summary>
        /// get by id async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(Guid key);

        /// <summary>
        /// get by id and return dto
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TDto GetByIdToDto<TDto>(Guid key) where TDto:class, new();
        /// <summary>
        /// get be id and return dto async
        /// </summary>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<TDto> GetByIdToDtoAsync<TDto>(Guid key) where TDto : class, new();
        /// <summary>
        /// get entity list by condition
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetEntities(Expression<Func<TEntity, bool>> whereLambda);

        /// <summary>
        /// get entities and order by column
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="IsAcs"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetEntities<s>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, s>> orderByLambda, bool IsAcs = true);

        /// <summary>
        /// get paged entities
        /// </summary>
        /// <typeparam name="s"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="IsAcs"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        IQueryable<TEntity> GetEntities<s>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, s>> orderByLambda, bool IsAcs = true, int pageIndex = 1, int pageSize = 10);

        IQueryable<TEntity> ExecuteSql(string sql, params object[] parameters);

        #endregion

        #region Create
        /// <summary>
        /// insert entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity);

        Task<int> InsertAsync(TEntity[] entities);

        Task<int> InsertAsync<TDto>(TDto dto,Func<TDto,Task> checkFunc =null, Func<TDto,TEntity,Task<TEntity>> insertFunc =null,Func<TEntity,TDto> completeFunc=null);
        int Insert(params TEntity[] entities);
        #endregion

        #region Update
        int Update(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity[] entities);
        #endregion

        #region Delete
        Task<int> DeleteAsync(Guid id);
        Task<int> DeleteAsync(TEntity entity);
        int Delete(params TEntity[] entities);
        Task<int> DeleteBatchAsync(Expression<Func<TEntity, bool>> whereLambda);
        #endregion
        #region CheckExist
        Task<bool> ExistAsync(Expression<Func<TEntity, bool>> whereLambda);
        #endregion
    }
}
