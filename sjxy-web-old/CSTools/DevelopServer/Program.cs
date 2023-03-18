using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DevelopServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateGenericHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateGenericHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(
                (webBuilder) =>
                {
                    webBuilder.UseStartup<Startup>();
                }
            );
        }
    }
}
