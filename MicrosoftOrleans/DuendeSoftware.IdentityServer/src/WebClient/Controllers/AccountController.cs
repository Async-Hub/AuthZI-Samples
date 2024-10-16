using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
	public class AccountController : Controller
	{
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();

			// Redirect to the home page after logout
			return RedirectToAction("Index", "Home");
		}
	}
}