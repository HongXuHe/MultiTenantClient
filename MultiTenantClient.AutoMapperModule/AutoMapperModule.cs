using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.Shared;
using MultiTenantClient.Shared.Attributes;
using MultiTenantClient.Shared.Modules;
using System;
using System.Linq;
using System.Reflection;

namespace MultiTenantClient.AutoMapper
{
    public class AutoMapperModule : BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            var assemblies = AssemblyHelper.GetAllAssemblies();
            var services = configureService.ServiceCollection;
            var types = assemblies.SelectMany(x => x.GetTypes()).Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<MultiTenantAutoMapperAttribute>() != null).ToArray();
            services.AddAutoMapper(mapper =>
            {
                CreateMapping(types, mapper);
                AddAllProfiles(mapper);
            }, assemblies, ServiceLifetime.Singleton);
            var mapper = services.BuildServiceProvider().GetRequiredService<IMapper>();
        }

        private void CreateMapping(Type[] sourceTypes, IMapperConfigurationExpression mapper)
        {
            foreach (var type in sourceTypes)
            {
                var attribute = type.GetCustomAttribute<MultiTenantAutoMapperAttribute>();
                foreach (var target in attribute.TargetTypes)
                {
                    mapper.CreateMap(type, target);
                }
            }
        }

        private void AddAllProfiles(IMapperConfigurationExpression mapper)
        {
            var assemblies = AssemblyHelper.GetAllAssemblies();
            foreach (var assem in assemblies)
            {
                var profileTypes = assem.GetTypes().Where(x => x.IsSubclassOf(typeof(Profile))).ToList();
                foreach (var profileType in profileTypes)
                {
                    mapper.AddProfile(profileType);
                }
            }
        }
    }
}
