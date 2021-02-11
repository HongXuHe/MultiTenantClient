using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public static class AppModuleExtensions
    {
        public static IServiceCollection AddApplication<T>(this IServiceCollection services) where T : IAppModule
        {
            AddApplication(services, typeof(T));
            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            IStartUpModule startUpModule = new StartUpModule(type, services);
            var obj = new ObjectAccessor<IApplicationBuilder>();
            services.AddSingleton(typeof(IObjectAccessor<IApplicationBuilder>), obj);
            services.AddSingleton(typeof(ObjectAccessor<IApplicationBuilder>), obj);
            startUpModule.ConfigureServices(services);
            return services;
        }

        public static IApplicationBuilder InitializeApplication(this IApplicationBuilder app)
        {
            var startUpModule = app.ApplicationServices.GetRequiredService<IStartUpModule>();
            app.ApplicationServices.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value = app;
            startUpModule.Initialize(app.ApplicationServices);

            return app;
        }
    }
}
