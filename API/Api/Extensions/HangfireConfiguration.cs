using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.PostgreSql;
using System;

namespace Api.Extensions;

public static class HangfireConfiguration
{
    public static IServiceCollection AddHangfireJob(this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                  .UseSimpleAssemblyNameTypeSerializer()
                  .UseRecommendedSerializerSettings()
                  .UsePostgreSqlStorage(
                      configuration.GetConnectionString("HangfireConnection"),
                      new PostgreSqlStorageOptions
                      {
                          SchemaName = "hangfire",
                          PrepareSchemaIfNecessary = true,
                          QueuePollInterval = TimeSpan.FromSeconds(5),
                          InvisibilityTimeout = TimeSpan.FromMinutes(5),
                          DistributedLockTimeout = TimeSpan.FromMinutes(10),
                          JobExpirationCheckInterval = TimeSpan.FromHours(1),
                      });
        });

        services.AddHangfireServer(options =>
        {
            options.WorkerCount = Environment.ProcessorCount * 5;
        });
        return services;


    }



}

public class HangfireAllowAll : Hangfire.Dashboard.IDashboardAuthorizationFilter
{
    public bool Authorize(Hangfire.Dashboard.DashboardContext context)
    {
        return true;
    }
}
