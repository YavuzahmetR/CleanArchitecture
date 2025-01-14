
using CleanArchitecture.Application.NewFolder;
using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Persistance.Context;
using CleanArchitecture.WebApi.Configurations;
using CleanArchitecture.WebApi.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Net.Mail;

namespace CleanArchitecture.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.InstallServices(builder.Configuration,builder.Host,typeof(IServiceInstaller).Assembly);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicy");
            app.UseExceptionMiddleware();

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
