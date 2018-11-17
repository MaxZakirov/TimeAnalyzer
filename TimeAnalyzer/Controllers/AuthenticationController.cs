using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Models;


namespace TimeAnalyzer.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IUserManager userManager;

        public AuthenticationController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]UserLoginModel loginInfo)
        {
            try
            {
                User userData = await this.userManager.Authenticate(this.HttpContext, loginInfo);
                return this.Ok(userData);
            }
            catch(IncorrectLogInInfoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await this.userManager.Logout(this.HttpContext);
            return this.Ok();
        }
    }
}