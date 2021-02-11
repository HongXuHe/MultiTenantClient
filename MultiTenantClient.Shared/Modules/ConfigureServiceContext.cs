using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public class ConfigureServiceContext
    {
        public ConfigureServiceContext(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public IServiceCollection ServiceCollection { get; set; }
    }
}
