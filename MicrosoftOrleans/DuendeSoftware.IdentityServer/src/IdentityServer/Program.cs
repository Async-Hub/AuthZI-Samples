using IdentityServer4;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddIdentityServer()
  .AddDeveloperSigningCredential()
  .AddInMemoryApiScopes(IdentityServerConfig.GetApiScopes())
  .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
  .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
  .AddInMemoryClients(IdentityServerConfig.GetClients())
  //.AddAspNetIdentity<ApplicationUser>()
  .AddTestUsers(IdentityServerConfig.GetUsers());

builder.Services.AddControllersWithViews(); ;

var app = builder.Build();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();

app.UseIdentityServer();
app.UseAuthorization();

//app.MapControllerRoute(
//  name: "default",
//  pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages().RequireAuthorization();

app.Run();
