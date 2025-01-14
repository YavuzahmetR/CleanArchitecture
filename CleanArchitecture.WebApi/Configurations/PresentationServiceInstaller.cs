using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace CleanArchitecture.WebApi.Configurations
{
    public sealed class PresentationServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.WithOrigins("https://localhost:7260/swagger/index.html").
                    WithMethods("POST").
                    AllowAnyHeader().
                    AllowCredentials();
                });
            });

            services.AddControllers().AddApplicationPart(assembly: typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(setup =>
            {
                var jwtSecuritySheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
            });
        }
    }
}
