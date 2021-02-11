using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantClient.API.StartUps
{
    [DependsOn(typeof(SqlServerModule))]
    public class APIWebModule:BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            var services = configureService.ServiceCollection;
            services.AddControllers();
        }

        public override void InitializationApplication(ConfigureContext context)
        {
           var builder = context.GetApplicationBuilder();
            builder.UseRouting();
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
