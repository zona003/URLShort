using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using URLShort.Models;
using Microsoft.EntityFrameworkCore;

namespace URLShort.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private UserContext _userContext;

        public LoginController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] (string login, string password) body)
        {
            var user = await _userContext.Users.FirstOrDefaultAsync(x => x.Login == body.login && x.Passwood == body.password);

            if (user == null)
            {
                return BadRequest("Invalid credentials.");
            }
            await Authenticate(user);

            return Ok(new User { Id = user.Id, Login = user.Login, Passwood = "", RoleId = user.RoleId });
        }


        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
                , new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(id));
        }
    }
}
