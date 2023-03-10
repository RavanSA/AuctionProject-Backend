namespace AuctionSystem.Infrastructure
{
    using Application.Common.Interfaces;
    using Common;
    using Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddScoped<IUserManager, UserManagerService>()
                .AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}