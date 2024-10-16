using Common;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebClient.Controllers;

namespace WebClient.User
{
  public class UserController(
    IHttpContextAccessor httpContextAccessor,
    ILogger<HomeController> logger) : Controller
  {
    public IActionResult Index()
    {
      return View("~/User/Index.cshtml");
    }

    [Authorize]
    public async Task<IActionResult> Profile()
    {
      var accessToken = await httpContextAccessor.HttpContext.GetTokenAsync("access_token");
      
      logger.LogInformation(new EventId(LogEvents.AccessTokenRetrieved),
        $"Access Token: successfully retrieved.");

      var httpClient = new HttpClient(HttpClientExtensions.CreateHttpClientHandler(true));
      httpClient.SetBearerToken(accessToken);

      string result;

      var response = await httpClient.GetAsync($"{Common.Config.ApiUrl}/api/User/Alice");
      if (response.IsSuccessStatusCode)
      {
        result = await response.Content.ReadAsStringAsync();
      }
      else
      {
        result = response.ReasonPhrase;
      }

      ViewBag.Response = result;

      return View("~/User/Profile.cshtml");
    }
  }
}