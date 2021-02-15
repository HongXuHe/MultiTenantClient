using MultiTenantClient.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.DependencyInterfaces
{
    /// <summary>
    /// implement this will have singleton instance
    /// </summary>
    [IgnoreDependency]
    public interface ISingletonDependency
    {
    }
}
