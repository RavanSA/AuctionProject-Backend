namespace Application
{
    using System.Reflection;
    using Application.Common.Abstraction;
    using Application.Common.Concrete;
    using Application.Common.Interfaces;
    using AutoMapper;
    using Common;
    using global::Common.AutoMapping.Profiles;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;

    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
           services.AddAutoMapper(cfg =>
            {
                 cfg.ShouldMapMethod = mi => false;
                cfg.ShouldUseConstructor = ci => false;
            }, typeof(DefaultProfile).Assembly);
            services
                .AddMediatR(Assembly.GetExecutingAssembly());

            services
              .AddScoped<IItemService, ItemService>();
            return services;
        }
    }
}