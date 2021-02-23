using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantClient.Aop;
using MultiTenantClient.Repo;
using MultiTenantClient.Shared.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiTenantClient.AutoMapper;
using MultiTenantClient.Swagger;
using MultiTenantClient.EventBus;
using MultiTenantClient.Repo.UOW;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MultiTenantClient.Shared.Extensions;

namespace MultiTenantClient.API.StartUps
{
    [DependsOn(typeof(SqlServerModule),
        typeof(AopModule),
        typeof(AutoMapperModule),
        typeof(SwaggerModule)
        )]
    public class APIWebModule : BaseModule
    {
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            var services = configureService.ServiceCollection;
            var config = services.GetConfiguration();
            services.AddControllers();
            services.AddAuthentication("Bearer")
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuerSigningKey = true,
                           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(config.GetSection("MultiTenantClient:tokenKey").Value)),
                           ValidateIssuer = false,
                           ValidateAudience = false
                       };
                   });
        }

        public override void InitializationApplication(ConfigureContext context)
        {
            var builder = context.GetApplicationBuilder();
            builder.UseRouting();
            builder.UseAuthentication();
            builder.UseAuthorization();
            builder.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
