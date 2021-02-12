using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MultiTenantClient.Shared.Modules
{
    public class StartUpModule : BaseModule, IStartUpModule
    {
        public Type StartUpModuleType { get; set; }

        public IServiceCollection Services { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        public IList<IAppModule> Modules { get; set; }
        List<IAppModule> _moduleListAll = new List<IAppModule>();

        public StartUpModule(Type startUpModuleType, IServiceCollection services)
        {
            //  services.TryAddSingleton<
            services.AddSingleton<IStartUpModule>(this);
            StartUpModuleType = startUpModuleType;
            Services = services;
            _moduleListAll = GetAllAppModules();
            Modules = LoadModules();
        }


        /// <summary>
        /// configure all module services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceCollectionContext = new ConfigureServiceContext(services);
            services.AddSingleton(serviceCollectionContext);
            foreach (var module in Modules)
            {
                services.AddSingleton(module);
                module.ConfigureServices(serviceCollectionContext);
            }
        }

        public void Dispose()
        {
            if (ServiceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public void Initialize(IServiceProvider app)
        {
            ServiceProvider = app;
            using (var scope = ServiceProvider.CreateScope())
            {
                var configurationContext = new ConfigureContext(app);
                foreach (var module in Modules)
                {
                    module.InitializationApplication(configurationContext);
                }
            }
        }


        private List<IAppModule> GetAllAppModules()
        {
            var modules = new List<IAppModule>();
            Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select((item) => Assembly.Load(item));
            var types = AssemblyHelper.GetAllAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => IsAppModule(p)).ToList();
            foreach (var type in types)
            {
                var module = CreatModuleInstanceAndSetSingleton(type, Services);
                if (!modules.Contains(module))
                {
                    modules.Add(module);
                }
            }
            return modules;
        }

        //load all its modules

        private IList<IAppModule> LoadModules()
        {
            List<IAppModule> moduleList = new List<IAppModule>();
            //check if startupmodule exist
            var existStartUpModule = _moduleListAll.FirstOrDefault(m => m.GetType() == StartUpModuleType);
            if (existStartUpModule == null)
            {
                throw new ArgumentNullException(nameof(StartUpModuleType));
            }
            moduleList.Add(existStartUpModule);
            //get all its dependent module
            var dependedsTypes = existStartUpModule.GetDependedTypes();
            foreach (var depended in dependedsTypes)
            {
                if (typeof(IAppModule).IsAssignableFrom(depended))
                {
                    //relay module
                    var existModule = _moduleListAll.FirstOrDefault(x => x.GetType() == depended);
                    if (existModule == null)
                    {
                        throw new ArgumentNullException($"cannot find module {depended.FullName}");
                    }
                    if (!moduleList.Contains(existModule))
                    {
                        moduleList.Add(existModule);
                    }
                }
            }
            return moduleList;

        }

        private IAppModule CreatModuleInstanceAndSetSingleton(Type type, IServiceCollection services)
        {
            var moduleInstance = (IAppModule)Activator.CreateInstance(type);/*(IAppModule)Expression.Lambda(Expression.New(type)).Compile().DynamicInvoke();*/
            services.AddSingleton(type, moduleInstance);
            return moduleInstance;
        }
    }
}
