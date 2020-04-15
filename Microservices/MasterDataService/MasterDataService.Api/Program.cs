using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MasterDataService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}
