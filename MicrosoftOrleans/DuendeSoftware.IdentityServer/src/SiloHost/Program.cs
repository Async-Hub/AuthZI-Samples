using Authzi.Identity.DuendeSoftware.IdentityServer;
using Authzi.MicrosoftOrleans.DuendeSoftware.IdentityServer;
using Common;
using GrainsInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SiloHost
{
	internal static class Program
  {
    public static async Task Main(string[] args)
    {
      try
      {
        Console.Title = "SiloHost";
        var host = CreateSiloHostBuilder(args);
        await host.RunAsync();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    private static IHost CreateSiloHostBuilder(string[] args)
    {
      var identityServerConfig = new IdentityServerConfig(Config.IdentityServerUrl,
					"Cluster", @"@3x3g*RLez$TNU!_7!QW", "Cluster");

      var builder = Host.CreateDefaultBuilder(args)
          .UseEnvironment(Environments.Staging)
          .ConfigureServices((hostContext, services) => { })
          .UseOrleans((context, siloBuilder) =>
          {
            siloBuilder.UseLocalhostClustering()
                      .ConfigureServices(services =>
                      {
                        services.AddOrleansAuthorization(identityServerConfig,
                                  config =>
                                  {
                                    config.ConfigureAuthorizationOptions = AuthorizationConfig.ConfigureOptions;
																		config.TracingEnabled = true;
                                    config.ConfigureSecurityOptions = options =>
                                      {
                                        //For not production environments only!
                                        options.RequireHttps = false;
                                      };
                                  });
                      });
          })
          .UseConsoleLifetime()
          // Configure logging with any logging framework that supports Microsoft.Extensions.Logging.
          // In this particular case it logs using the Microsoft.Extensions.Logging.Console package.
          .ConfigureLogging(loggingBuilder =>
          {
            loggingBuilder.AddConsole();
          });

      var host = builder.Build();
      var logger = host.Services.GetService<ILoggerFactory>().CreateLogger<ILogger>();
      HostInfo.Log(logger);

      return host;
    }
  }
}