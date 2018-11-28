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
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationManager userManager;

        public AuthenticationController(IAuthenticationManager userManager)
        {
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn([FromBody]UserLoginModel loginInfo)
        {
            try
            {
                User userData = await this.userManager.Authenticate(loginInfo);
                return this.Ok(userData);
            }
            catch(IncorrectLogInInfoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public IActionResult Check()
        {
            return Ok("It's working");
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult CheckIn([FromBody]UserCheckinModel loginInfo)
        {
            try
            {
                User userData = this.userManager.CheckIn(loginInfo);
                return this.Ok(userData);
            }
            catch(DuplicateNameException ex)
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