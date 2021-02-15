using MultiTenantClient.Shared.Modules;
using System;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using System.IO;
using MultiTenantClient.Shared.Extensions;
using System.Collections.Generic;

namespace MultiTenantClient.Swagger
{
    public class SwaggerModule : BaseModule
    {
        private string _url = string.Empty;
        private string _title = string.Empty;
        private string _version = string.Empty;
        public override void ConfigureServices(ConfigureServiceContext configureService)
        {
            var services = configureService.ServiceCollection;
            var Configuration = services.GetConfiguration();
            _title = Configuration["MultiTenantClient:Swagger:title"];
            _version = Configuration["MultiTenantClient:Swagger:version"];
            _url = Configuration["MultiTenantClient:Swagger:url"];
            if (_title == null || _version == null || _url == null)
            {
                throw new ArgumentNullException("Swagger title, url, version cannot be null");
            }
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_version, new OpenApiInfo
                {
                    Version = _version,
                    Title = _title,
                });
                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var files = Directory.GetFiles(basePath, "*.xml");
                foreach (var fiel in files)
                {
                    c.IncludeXmlComments(fiel, true);
                }

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization Header using the Bearer scheme",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                 {
                     new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                             {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"
                             }
                         },
                        new string[] { }
                  }
                });
            });
        }
        public override void InitializationApplication(ConfigureContext context)
        {
            var appProvider = context.ServiceProvider;
            var builder = appProvider.GetRequiredService<IObjectAccessor<IApplicationBuilder>>().Value;
            builder.UseStaticFiles();
            builder.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(_url, $"{_version}");
                c.RoutePrefix = "";
            });
        }
    }
}
