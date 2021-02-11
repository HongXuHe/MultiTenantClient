using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public interface IStartUpModule:IDisposable
    {
        Type StartUpModuleType { get; }
        IServiceCollection Services { get; }
        IServiceProvider ServiceProvider { get; }
        IList<IAppModule> Modules { get; }
        void ConfigureServices(IServiceCollection services);
        void Initialize(IServiceProvider app);
    }
}
