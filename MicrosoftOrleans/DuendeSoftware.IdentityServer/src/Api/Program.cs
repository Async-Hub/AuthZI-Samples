using Api;
using Api.Orleans;
using Authzi.Identity.DuendeSoftware.IdentityServer;
using Authzi.MicrosoftOrleans.DuendeSoftware.IdentityServer;
using Authzi.Security;
using Common;
using GrainsInterfaces;
//using IdentityModel.AspNetCore.AccessTokenValidation;
using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Abstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Orleans;
using Orleans.Hosting;

var builder = WebApplication.CreateBuilder(args);

//IdentityServer credentials. Do not use this for production!
var apiIdentityServerConfig = new IdentityServerConfig(Config.IdentityServerUrl,
    "Api1", @"TFGB=?Gf3UvH+Uqfu_5p", "Cluster");

var clusterIdentityServerConfig = new IdentityServerConfig(Config.IdentityServerUrl,
    "Cluster", "@3x3g*RLez$TNU!_7!QW", "Cluster");

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddAuthentication("token")
  // JWT tokens
  .AddJwtBearer("token", options =>
  {
    // For development environments only. Do not use for production.
    options.RequireHttpsMetadata = false;

    options.Authority = apiIdentityServerConfig.Url;
    options.Audience = "Api1";
    options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateAudience = false
    };
    // if token does not contain a dot, it is a reference token
    // https://leastprivilege.com/2020/07/06/flexible-access-token-validation-in-asp-net-core/
    options.ForwardDefaultSelector = Extensions.ForwardReferenceToken("introspection");
  })
  .AddOAuth2Introspection("introspection", options =>
  {
    options.Authority = apiIdentityServerConfig.Url;

    // this maps to the API resource name and secret
    options.ClientId = apiIdentityServerConfig.ClientId;
    options.ClientSecret = apiIdentityServerConfig.ClientSecret;
  });
//// reference tokens
//.AddOAuth2Introspection("introspection", options =>
//{
//    options.Authority = apiIdentityServerConfig.Url;
//    // For development environments only. Do not use for production.
//    options.DiscoveryPolicy.RequireHttps = true;
//    options.ClientId = apiIdentityServerConfig.ClientId;
//    options.ClientSecret = apiIdentityServerConfig.ClientSecret;
//});

builder.Services.AddControllers();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//builder.Services.AddHttpClient(OAuth2IntrospectionDefaults.BackChannelHttpClientName)
//    .ConfigurePrimaryHttpMessageHandler(() => CreateHttpClientHandler(true));

builder.UseOrleansClient(client =>
{
  client.UseLocalhostClustering().ConfigureServices(services =>
  {
    services.AddOrleansClientAuthorization(clusterIdentityServerConfig, config =>
    {
      config.ConfigureAuthorizationOptions = AuthorizationConfig.ConfigureOptions;
      config.ConfigureAccessTokenVerifierOptions = options =>
      {
        options.InMemoryCacheEnabled = true;
      };
      config.ConfigureSecurityOptions = options =>
      {
        //For not production environments only!
        options.RequireHttps = false;
      };

      config.TracingEnabled = true;
    });

    //services.AddSingleton<Func<IHttpContextAccessor>>(serviceProvider => () => _httpContextAccessor);
    services.AddTransient<IAccessTokenProvider, AspNetCoreAccessTokenProvider>();
  });
});

//// ReSharper disable once RedundantTypeArgumentsOfMethod
//builder.Services.AddSingleton<IClusterClient>(serviceProvider =>
//{
//    var logger = serviceProvider.GetRequiredService<ILogger<IClusterClient>>();
//    var telemetryClient = serviceProvider.GetRequiredService<TelemetryClient>();

//    var provider = new OrleansClusterClientProvider(
//        serviceProvider.GetService<IHttpContextAccessor>(),
//        logger, apiIdentityServerConfig, simpleClusterAzureStorageConnection,
//        telemetryClient);

//    provider.StartClientWithRetries(out var client);

//    return client;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();