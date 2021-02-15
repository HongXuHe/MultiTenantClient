using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.EventBus;
using MultiTenantClient.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantClient.API.StartUps
{
    [DependsOn(typeof(EventBustModule))]
    public class SqlServerModule : SqlServerModuleBase
    {
        protected override IServiceCollection AddRepository(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        protected override IServiceCollection AddUnitOfWork(IServiceCollection services)
        {
            throw new NotImplementedException();
        }

        protected override IServiceCollection UseSql(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
