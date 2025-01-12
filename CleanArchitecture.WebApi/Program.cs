
using CleanArchitecture.Application.NewFolder;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Services;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.Persistance.Repositories;
using CleanArchitecture.Persistance.Services;
using CleanArchitecture.Presentation;
using CleanArchitecture.WebApi.Middleware;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.WebApi.OptionsSetup;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CleanArchitecture.Application.Abstractions;
using Microsoft.OpenApi.Models;

namespace CleanArchitecture.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddTransient<ExceptionMiddleware>();
            builder.Services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyReference).Assembly);
            var smtpConfig = builder.Configuration.GetSection("SmtpSettings");
            builder.Services.AddFluentEmail(smtpConfig["FromAddress"]).AddSmtpSender(new SmtpClient(smtpConfig["Host"])
            {
                Port = int.Parse(smtpConfig["Port"]!),
                Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["AppPassword"]),
                EnableSsl = true
            });
            builder.Services.AddSingleton<IEmailService, EmailService>();

            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

            builder.Services.AddAuthentication().AddJwtBearer();
            builder.Services.AddAuthorization();


            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IRoleService, RoleService>();
            builder.Services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            builder.Services.AddScoped<IUserRoleService, UserRoleService>();

            builder.Services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());
            builder.Services.AddControllers().AddApplicationPart(assembly: typeof(CleanArchitecture.Presentation.AssemblyReference).Assembly);

            builder.Services.AddMediatR(cfr => cfr.RegisterServicesFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly));
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            builder.Services.AddValidatorsFromAssembly(typeof(CleanArchitecture.Application.AssemblyReference).Assembly);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(setup =>
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


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
