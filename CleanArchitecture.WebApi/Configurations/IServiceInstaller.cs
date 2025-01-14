namespace CleanArchitecture.WebApi.Configurations
{
    public interface IServiceInstaller  
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder);
    }
}
