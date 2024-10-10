using Common;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebClient.Controllers;

namespace WebClient.User
{
    public class UserController : Controller
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryClient _telemetryClient;

        public UserController(ITokenAcquisition tokenAcquisition,
            ILogger<HomeController> logger, TelemetryClient telemetryClient)
        {
            _tokenAcquisition = tokenAcquisition;
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        public IActionResult Index()
        {
            return View("~/User/Index.cshtml");
        }

        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(Config.Api1Scopes);
            _logger.LogInformation(new EventId(LogEvents.AccessTokenRetrieved),
                $"Access Token: successfully retrieved.");

            var httpClient = new HttpClient(HttpClientExtensions.CreateHttpClientHandler(true));
            httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("Bearer", accessToken);

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
