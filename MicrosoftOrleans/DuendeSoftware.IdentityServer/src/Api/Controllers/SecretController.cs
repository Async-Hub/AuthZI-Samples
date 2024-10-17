using GrainsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController, Authorize]
	[Route("api/[controller]/{userName}")]
	public class SecretController(IClusterClient clusterClient,
		ILogger<SecretController> logger) : Controller
	{
		private readonly ILogger<SecretController> _logger = logger;

		[HttpGet]
		public async Task<ActionResult<string>> Get(string userName)
		{
			var grain = clusterClient.GetGrain<IUserGrain>(userName);

			return await grain.TakeSecret();
		}
	}
}