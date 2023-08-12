using Microsoft.AspNetCore;

namespace ProyectoDiWork
{
    public class Program
    {
        public static IServiceProvider ServiceProvider;
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var host = WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
             .Build();
            ServiceProvider = host.Services;
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
