using CleanArchitecture.Application.NewFolder;
using FluentValidation;
using MediatR;

namespace CleanArchitecture.WebApi.Configurations
{
    public class ApplicationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);
        }
    }
}
