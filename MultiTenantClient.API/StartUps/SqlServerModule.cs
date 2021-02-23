using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.Entities;
using MultiTenantClient.Entities.Identity;
using MultiTenantClient.EventBus;
using MultiTenantClient.Repo;
using MultiTenantClient.Repo.UOW;
using MultiTenantClient.Shared;
using MultiTenantClient.Shared.Extensions;
using MultiTenantClient.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantClient.API.StartUps
{
    [DependsOn(typeof(EventBustModule))]
    public class SqlServerModule : SqlServerModuleBase
    {
        protected override IServiceCollection AddRepository(IServiceCollection services)
        {
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IRoleRepo, RoleRepo>();
            services.AddScoped<IPermissionRepo, PermissionRepo>();
            return services;
        }

        protected override IServiceCollection AddUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        protected override IServiceCollection UseSql(IServiceCollection services)
        {
            var config = services.GetConfiguration();
            var connStr = config["MultiTenantClient:SqlServer"];
            if (string.IsNullOrEmpty(connStr))
            {
                throw new ArgumentNullException("SqlServer connection String Error");
            }
            services.AddDbContext<MultiTenantClientContext>(options =>
            {
                options.UseSqlServer(connStr);
            });
            InitData(services.BuildServiceProvider());
            return services;
        }

        private void InitData(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MultiTenantClientContext>();
                context.Database.EnsureCreated();
                if (!context.UserEntities.Any())
                {
                    var permissionEntity = new PermissionEntity
                    {
                        PermissionName = "Admin",
                        PermissionValue = "Admin"
                    };
                    var roleEntity = new RoleEntity
                    {
                        Name = "Admin",
                        Permissions = new List<Role_Permission>
                    {
                        new Role_Permission
                        {
                            PermissionId =permissionEntity.Id,
                            PermissionEntity =permissionEntity
                        }
                    }
                    };
  
                    var userEntity = new UserEntity
                    {
                        Name = "Admin",
                        Email = "nwi@nationalweighing.com.au",
                        Password = HashStringHelper.CreateMD5("Admin"),
                        Roles = new List<User_Role>
                         {
                             new User_Role
                             {
                                 RoleId =roleEntity.Id,
                                 RoleEntity =roleEntity
                             }
                         },
                        Permissions = new List<User_Permission>
                    {
                        new User_Permission
                             {
                                 PermissionId =permissionEntity.Id,
                                PermissionEntity =permissionEntity
                             }
                    }
                    };
                    context.Add(userEntity);
                    context.SaveChanges();
                }
            }
        }
    }
}
