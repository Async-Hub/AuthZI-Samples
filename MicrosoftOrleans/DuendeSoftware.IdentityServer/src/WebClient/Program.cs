using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddHttpClient();

builder.Services.AddSingleton<IDiscoveryCache>(serviceProvider =>
{
    var factory = serviceProvider.GetRequiredService<IHttpClientFactory>();

    return new DiscoveryCache(Common.Config.IdentityServerUrl,
        () => factory.CreateClient());
});

builder.Services.AddHttpClient("api", client =>
{
    client.BaseAddress = new Uri(Common.Config.ApiUrl);
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        // For development environments only. Do not use for production.
        options.RequireHttpsMetadata = false;

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        options.Authority = Common.Config.IdentityServerUrl;
        options.ClientId = "WebClient";
        options.ClientSecret = "pckJ#MH-9f9K?+^Bzx&4";

        options.ResponseType = "code";
        options.UsePkce = true;
        options.SaveTokens = true;

        options.Scope.Add("Api1");
        options.Scope.Add("Cluster");
        options.Scope.Add("Api1.Read");
        options.Scope.Add("Api1.Write");

        options.Scope.Add("offline_access");

        //var isNonProductionEnvironment = _env.IsDevelopment() || _env.IsStaging();
        //options.BackchannelHttpHandler = CreateHttpClientHandler(true);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
