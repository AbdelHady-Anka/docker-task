using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Actio.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IWebHost _webHost;

        public ServiceHost(IWebHost webHost)
        {
            _webHost = webHost;
        }

        public void Run() => _webHost.Run();

        public static HostBuilder CreateHostBuidler<TStartup>(string[] args) where TStartup : class
        {
            Console.Title = typeof(TStartup).Namespace;

            return new HostBuilder(WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddEnvironmentVariables()
                        .AddCommandLine(args)
                        .Build();
                })
                    .UseStartup<TStartup>()
                    .Build());
        }
    }
}