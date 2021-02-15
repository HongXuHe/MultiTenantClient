using MultiTenantClient.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.DependencyInterfaces
{
    /// <summary>
    /// implements this will di as scope
    /// </summary>
    [IgnoreDependency]
    public interface IScopedDependency
    {
    }
}
