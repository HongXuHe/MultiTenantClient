using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public static class ConfigureContextExtensions
    {
        public static IApplicationBuilder GetApplicationBuilder(this ConfigureContext context)
        {
            var provider = context.ServiceProvider;
            return provider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
        }
    }
}
