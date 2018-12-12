using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Models;


namespace TimeAnalyzer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationManager userManager;

        public AuthenticationController(IAuthenticationManager userManager)
        {
            this.userManager = userManager;
        }

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
    }
}