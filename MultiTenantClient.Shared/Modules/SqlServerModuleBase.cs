using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public abstract class SqlServerModuleBase : BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            UseSql(configureService.ServiceCollection);
            AddUnitOfWork(configureService.ServiceCollection);
            AddRepository(configureService.ServiceCollection);
        }
        protected abstract IServiceCollection AddUnitOfWork(IServiceCollection services);
        protected abstract IServiceCollection AddRepository(IServiceCollection services);
        protected abstract IServiceCollection UseSql(IServiceCollection services);
    }
}
