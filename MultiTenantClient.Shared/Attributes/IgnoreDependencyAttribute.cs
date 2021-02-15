using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Attributes
{
    /// <summary>
    /// this attribute will ignore dependency injection
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Interface)]
    public class IgnoreDependencyAttribute : Attribute
    {
    }
}
