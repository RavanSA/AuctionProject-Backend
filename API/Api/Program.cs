namespace Api
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    //dotnet ef migrations add changes-entity --project ../Infrastructure/Persistence --startup-project API
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}