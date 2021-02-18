using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultiTenantClient.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MultiTenantClientContext _dbContext;
        private readonly ILogger<UnitOfWork> _logger;
        public UnitOfWork(MultiTenantClientContext dbContext, ILogger<UnitOfWork> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// begin transaction but not committed
        /// </summary>
        /// <param name="action"></param>
        public bool BeginTransaction(Action action)
        {
            using (var tran = _dbContext.Database.BeginTransaction())
            {

                try
                {
                    action?.Invoke();
                    tran.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tran.Rollback();
                    _logger.LogError(e.Message, e);
                    return false;
                }

            }
        }

        public async Task<bool> BeginTransactionAsync(Action action)
        {
            using (var tran = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    action?.Invoke();
                    await tran.CommitAsync();
                    return await Task.FromResult(true);
                }
                catch (Exception e)
                {
                    await tran.RollbackAsync();
                    _logger.LogError(e.Message, e);
                    return await Task.FromResult(false);
                }

            }
        }
        public MultiTenantClientContext GetDbContext()
        {
            return _dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
