using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Runtime.Loader;

namespace MultiTenantClient.Shared
{
    public class AssemblyHelper
    {
        public static List<Assembly> GetAllAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            string[] filters =
            {
                "mscorlib",
                "netstandard",
                "dotnet",
                "api-ms-win-core",
                "runtime.",
                "System",
                "Microsoft",
                "Window",
            };
            var deps = DependencyContext.Default;
            //get all self-defined classlibraries
            var libs = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" && !filters.Any(lib.Name.StartsWith));
            try
            {
                foreach (var lib in libs)
                {
                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(lib.Name));
                    list.Add(assembly);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return list;
        }
    }
}
