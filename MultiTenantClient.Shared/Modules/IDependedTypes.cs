using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public interface IDependedTypes
    {
        Type[] GetDependedTypes();
    }
}
