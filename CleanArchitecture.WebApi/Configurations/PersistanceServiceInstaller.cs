using CleanArchitecture.Domain.Entitites;
using CleanArchitecture.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Serilog;
using Serilog.Sinks.MSSqlServer;

namespace CleanArchitecture.WebApi.Configurations
{
    public sealed class PersistanceServiceInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration, IHostBuilder hostBuilder)
        {
            services.AddAutoMapper(typeof(CleanArchitecture.Persistance.AssemblyReference).Assembly);

            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<AppDbContext>();

            var smtpConfig = configuration.GetSection("SmtpSettings");
            services.AddFluentEmail(smtpConfig["FromAddress"]).AddSmtpSender(new SmtpClient(smtpConfig["Host"])
            {
                Port = int.Parse(smtpConfig["Port"]!),
                Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["AppPassword"]),
                EnableSsl = true
            });

            var columnOpts = new ColumnOptions();
            columnOpts.Store.Remove(StandardColumn.Properties);
            columnOpts.Store.Add(StandardColumn.LogEvent);
            columnOpts.LogEvent.DataLength = 2048;
            columnOpts.PrimaryKey = columnOpts.TimeStamp;
            columnOpts.TimeStamp.NonClusteredIndex = true;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
                sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true },null
                , null, restrictedToMinimumLevel:Serilog.Events.LogEventLevel.Information,columnOptions:columnOpts).CreateLogger();

            hostBuilder.UseSerilog();

        }
    }
}
