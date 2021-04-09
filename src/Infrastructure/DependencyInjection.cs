using Application.Common.Interfaces;
using Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Services;
using Infrastructure.Queues;
using Application.Common.Queues;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseNpgsql(connectionString);
            });

            services.AddHangfire(hfConfig => hfConfig
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(configuration.GetConnectionString("DefaultConnection"), new PostgreSqlStorageOptions
                    {
                        SchemaName = "hangfire"
                    }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IBackgroundWorkerService, BackgroundWorkerService>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailQueue, EmailQueue>();
            services.AddScoped<IEmailConsumer, EmailConsumer>();

            return services;
        }
    }
}