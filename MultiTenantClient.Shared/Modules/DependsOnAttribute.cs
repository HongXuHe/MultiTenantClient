using System;
using System.Collections.Generic;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =true)]
    public class DependsOnAttribute:Attribute, IDependedTypes
    {
        public DependsOnAttribute(params Type[] types)
        {
            DependedTypes = types ?? throw new ArgumentNullException(nameof(types));
        }

        private Type[] DependedTypes { get; }

        public Type[] GetDependedTypes()
        {
            return DependedTypes;
        }
    }
}
