using MultiTenantClient.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantClient.API.StartUps
{
    public class SqlServerModule : BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
        }

        public override void InitializationApplication(ConfigureContext context)
        {
        }
    }
}
