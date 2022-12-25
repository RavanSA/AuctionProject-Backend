namespace Application
{
    using System.Reflection;
    using AutoMapper;
    using Common;
    using global::Common.AutoMapping.Profiles;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(DefaultProfile));
            services
                .AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}