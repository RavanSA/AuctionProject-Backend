//namespace Api
//{
//    using Microsoft.AspNetCore.Hosting;
//    using Microsoft.Extensions.Hosting;
//    //dotnet ef migrations add changes-entity --project ../Infrastructure/Persistence --startup-project API
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        private static IHostBuilder CreateHostBuilder(string[] args) =>
//            Host.CreateDefaultBuilder(args)
//                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
//    }
//}

using Api.Extensions;
using Api.Hubss;
using Api.Jobs;
using Application;
using Application.Common.Abstraction;
using Application.Common.Concrete;
using AuctionSystem.Infrastructure;
 using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Persistence;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;

 
JsonConvert.DefaultSettings = () => new JsonSerializerSettings
{
    ContractResolver = new CamelCasePropertyNamesContractResolver()
};

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Host.UseSerilog((context, services, logger) =>
{
    logger
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

services
    .AddPersistence(configuration)
    .AddInfrastructure(configuration)
    .AddApplication()
    .AddJwtAuthentication(services.AddJwtSecret(configuration))
    .AddRequiredServices()
    .AddSwagger()
    .AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder
                .SetIsOriginAllowed(_ => true)
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

services.AddControllers()
    .AddNewtonsoftJson(o => o.UseCamelCasing(true))
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    });

services.AddHangfire(config =>
{
    config.UseMemoryStorage();
});
services.AddHangfireServer();

services.AddScoped<IFcmPushService, FcmPushService>();
services.AddScoped<RecurringQueue>();

services.AddSignalR(o =>
{
    o.EnableDetailedErrors = true;
});

 
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(
        configuration.GetValue<string>("Firebase:ServiceAccountPath"))
});

 
var app = builder.Build();

 
var autoMigSetting = configuration["auto-mig"];
if (bool.TryParse(autoMigSetting, out var autoMig) && autoMig)
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AuctionSystemDbContext>();
    db.Database.Migrate();
}

 
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

 
using (var scope = app.Services.CreateScope())
{
    var scheduler = scope.ServiceProvider.GetRequiredService<RecurringQueue>();
    scheduler.ScheduleJobs();
}

 
app
    //.UseHttpsRedirection()
    .UseRouting()
    .UseCors()
    .UseMiddleware<ExceptionMiddleware>()
    .UseAuthentication()
    .UseAuthorization()
    .UseSwagger()
    .UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auction API v1");
        c.RoutePrefix = "swagger";
    });

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAllowAll() }
});

app.MapControllers();
app.MapHub<MessageHub>("/chathub");

app.Run();
