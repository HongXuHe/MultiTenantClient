using MediatR;
using MultiTenantClient.Shared;
using MultiTenantClient.Shared.Modules;
using System;

namespace MultiTenantClient.EventBus
{
    public class EventBustModule:BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            var services = configureService.ServiceCollection;
            services.AddMediatR(AssemblyHelper.GetAllAssemblies().ToArray());
        }
    }
}
