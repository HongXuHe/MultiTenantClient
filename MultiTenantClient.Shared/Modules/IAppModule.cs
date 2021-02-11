using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public interface IAppModule : IAppInit
    {
        /// <summary>
        /// configure services
        /// </summary>
        /// <param name="configureService"></param>
        void ConfigureServices(ConfigureServiceContext configureService);

        /// <summary>
        /// Get depended module types
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        Type[] GetDependedTypes(Type moduleType = null);
    }
}
