using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public abstract class BaseModule : IAppModule
    {
        public virtual void ConfigureServices(ConfigureServiceContext configureService)
        {
        }


        public virtual void InitializationApplication(ConfigureContext context)
        { }


        /// <summary>
        /// get depended types of certain module
        /// </summary>
        /// <param name="moduleType"></param>
        /// <returns></returns>
        public Type[] GetDependedTypes(Type moduleType = null)
        {
            //if null the runing module
            if (moduleType == null)
            {
                moduleType = GetType();
            }
            var dependedTypes = moduleType.GetCustomAttributes().OfType<IDependedTypes>().ToArray();
            if (dependedTypes.Length == 0)
            {
                return new Type[0];
            }
            var dependList = new List<Type>();
            foreach (var depend in dependedTypes)
            {
                var dependeds = depend.GetDependedTypes();
                if (dependeds.Length == 0)
                {
                    continue;
                }
                dependList.AddRange(dependeds);
                foreach (var d in dependeds)
                {
                    dependList.AddRange(GetDependedTypes(d));
                }
            }

            return dependList.Distinct().ToArray();

        }

        public static bool IsAppModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass &&
                    !typeInfo.IsGenericType &&
                    !typeInfo.IsAbstract &&
                    !typeof(IStartUpModule).IsAssignableFrom(type) &&
                    typeof(IAppModule).IsAssignableFrom(type);

        }
    }
}
