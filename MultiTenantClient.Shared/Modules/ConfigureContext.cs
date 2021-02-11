using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    /// <summary>
    /// ApplicationContext is used to use services among all modules
    /// </summary>
    public class ConfigureContext
    {
        public ConfigureContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public IServiceProvider ServiceProvider { get; set; }
    }
}
