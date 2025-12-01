namespace Api
{
    using Api.Hubss;
    using Application;
    using Application.Users.Commands.CreateUser;
    using AuctionSystem.Infrastructure;
    using Extensions;
     using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Persistence;
    using System;

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
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

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
;
            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });

            services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
        {
            var autoMigSetting = config["auto-mig"];
            bool autoMig = false;

            if (!string.IsNullOrEmpty(autoMigSetting))
                bool.TryParse(autoMigSetting, out autoMig);

            if (autoMig)
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AuctionSystemDbContext>();
                    dbContext.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            
            app
                //.UseHttpsRedirection()
                .UseRouting()
              //  .UseHsts()
                .UseCors()
                .UseMiddleware<ExceptionMiddleware>()
                .UseAuthentication()
                .UseAuthorization()
.UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API v1");
                c.RoutePrefix = "swagger";
            })              .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapHub<MessageHub>("/chathub");

                });

           
        }
    }
}