using Microsoft.AspNetCore;

namespace ProyectoDiWork
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Service provider
        /// </summary>
        public static IServiceProvider ServiceProvider;

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            var host = WebHost.CreateDefaultBuilder(args)
             .UseStartup<Startup>()
             .Build();
            ServiceProvider = host.Services;
            host.Run();
        }

        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
