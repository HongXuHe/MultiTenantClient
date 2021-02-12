using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Extensions
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// get configuration
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {
           return GetService<IConfiguration>(services);
        }

        /// <summary>
        /// get hosting environment
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IHostingEnvironment GetHostingEnvironment(this IServiceCollection services)
        {
            return GetService<IHostingEnvironment>(services);
        }
       public static T GetService<T>(IServiceCollection services)
        {
            return services.BuildServiceProvider().GetRequiredService<T>();
        }

       
    }
}
