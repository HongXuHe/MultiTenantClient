using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MultiTenantAutoMapperAttribute : Attribute
    {
        public MultiTenantAutoMapperAttribute(params Type[] targetTypes)
        {
            TargetTypes = targetTypes;
        }

        public Type[] TargetTypes { get; }
    }
}
