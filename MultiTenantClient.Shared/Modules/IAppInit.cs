using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public interface IAppInit
    {
        void InitializationApplication(ConfigureContext context);
    }
}
