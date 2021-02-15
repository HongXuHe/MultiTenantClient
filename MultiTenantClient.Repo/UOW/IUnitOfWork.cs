using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext GetDbContext();
        void BeginTransaction();
        void Commit();
        Task BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken));
        void Rollback();
        void UseTran(Action action);
        Task UseTranAsync(Func<Task> action);
        bool UseTran(Func<bool> func);
        Task<bool> UseTranAsync(Func<Task<bool>> func);
        Task CommitAsync();
        Task RollbackAsync();
    }
}
