using System.Reflection;

namespace CleanArchitecture.WebApi.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection InstallServices(this IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder, params Assembly[] assemblies)
        {
            List<IServiceInstaller> installers = typeof(DependencyInjection).Assembly.ExportedTypes
                .Where(x => typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList();
            installers.ForEach(installer => installer.InstallServices(services, configuration,hostBuilder));
            return services;
        }
    }
}
