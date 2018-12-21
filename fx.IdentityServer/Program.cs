using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace fx.IdentityService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "IdentityServer";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://0.0.0.0:5100")
                .UseContentRoot(Directory.GetCurrentDirectory())
               // .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
