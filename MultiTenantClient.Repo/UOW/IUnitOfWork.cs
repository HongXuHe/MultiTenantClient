using Microsoft.EntityFrameworkCore;
using MultiTenantClient.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo.UOW
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// get dbcontext
        /// </summary>
        /// <returns></returns>
        MultiTenantClientContext GetDbContext();

        /// <summary>
        /// begin a new tranaction
        /// </summary>
        bool BeginTransaction(Action action);
        /// <summary>
        /// commit everything
        /// </summary>
        void Commit();
        Task<int> CommitAsync();

        /// <summary>
        /// begin a new transaction async
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> BeginTransactionAsync(Action action);

    }
}
