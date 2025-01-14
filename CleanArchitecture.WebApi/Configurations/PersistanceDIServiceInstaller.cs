
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.WebApi.Middleware;
using GenericRepository;

namespace CleanArchitecture.WebApi.Configurations
{
    public sealed class PersistanceDIServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            services.AddTransient<ExceptionMiddleware>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserRoleService, UserRoleService>();
            services.AddSingleton<IEmailService, EmailService>();
        }
    }
}
