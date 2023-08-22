using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace URLShort.Controllers
{
    [Route("api/logout")]
    [ApiController]
    public class LogoutController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
