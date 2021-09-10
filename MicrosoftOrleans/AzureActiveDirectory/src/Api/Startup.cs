using Api.Orleans;
using Common;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Orleans;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, 
            IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        // For more information on how to configure your application,
        // visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Read Azure Storage connection string.
            var simpleClusterAzureStorageConnection =
                Environment.GetEnvironmentVariable(EnvironmentVariables.SimpleClusterAzureStorageConnection);

            // How to secure a Web API built with ASP.NET Core using the Microsoft identity platform
            // https://github.com/Azure-Samples/active-directory-aspnetcore-webapp-openidconnect-v2/tree/master/4-WebApp-your-API/4-1-MyOrg

            services.AddMicrosoftIdentityWebApiAuthentication(_configuration);

            services.AddControllers();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ReSharper disable once RedundantTypeArgumentsOfMethod
            services.AddSingleton<IClusterClient>(serviceProvider =>
            {
                var logger = serviceProvider.GetRequiredService<ILogger<IClusterClient>>();
                var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

                var provider = new OrleansClusterClientProvider(
                    serviceProvider.GetService<IHttpContextAccessor>(),
                    logger, Config.ApiClientCredentials, simpleClusterAzureStorageConnection,
                    telemetryClient);

                provider.StartClientWithRetries(out var client);

                return client;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
