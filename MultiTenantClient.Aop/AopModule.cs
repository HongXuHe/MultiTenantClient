using AspectCore.Configuration;
using AspectCore.DynamicProxy;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.Shared;
using MultiTenantClient.Shared.Extensions;
using MultiTenantClient.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MultiTenantClient.Aop
{
    public class AopModule:BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            var services = configureService.ServiceCollection;
           var Config= services.GetConfiguration();
            //find all the types of class is subclass of AbstractInterceptor
            var types = AssemblyHelper.GetAllAssemblies().SelectMany(s => s.GetTypes())
                .Where(t =>
                !t.IsAbstract &&
                !t.IsInterface &&
                t.IsSubclassOf(typeof(AbstractInterceptor))).ToList();
            var AOPModule = Config.GetSection("MultiTenantClient:InterceptorsModule").Value;
            var aopEnabled =Convert.ToBoolean( Config.GetSection("MultiTenantClient:InterceptorsModuleEnabled").Value);
            if (types?.Count > 0 && AOPModule !=null && aopEnabled)
            {
                foreach (var item in types)
                {
                    services.ConfigureDynamicProxy(config =>
                    {
                        config.Interceptors.AddTyped(item,Predicates.ForNameSpace(AOPModule) /*Predicates.ForNameSpace("MultiTenantClient.Aop.Test")*/);
                    });
                }
            }
        }
        public override void InitializationApplication(ConfigureContext context)
        {
            //base.InitializationApplication(context);
        }
    }
}
