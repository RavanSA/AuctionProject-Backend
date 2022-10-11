namespace Api
{
    using Api.Hubss;
    using Application;
    using Application.Users.Commands.CreateUser;
    using AuctionSystem.Infrastructure;
    using Extensions;
    using FluentValidation.AspNetCore;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Persistence;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services
                .AddPersistence(this.Configuration)
                .AddInfrastructure(this.Configuration)
                .AddApplication()
                .AddJwtAuthentication(services.AddJwtSecret(this.Configuration))
                .AddRequiredServices()
                .AddSwagger()
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.WithOrigins("http://localhost:3000",
                                "https://localhost:3000");
                            builder.SetIsOriginAllowed((hosts) => true);
                    builder.AllowCredentials();
                            builder.AllowAnyMethod();
                            builder.AllowAnyHeader();
                        });
                })
                .AddControllers()
                .AddNewtonsoftJson(options => options.UseCamelCasing(true))
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateUserCommandValidator>());

            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseDeveloperExceptionPage()
                .UseHttpsRedirection()
                .UseRouting()
                .UseHsts()
                .UseCors()
                .UseAuthentication()
                .UseAuthorization()
                .UseSwaggerUi()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<MessageHub>("/chathub");

                });

           
        }
    }
}