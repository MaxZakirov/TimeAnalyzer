using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Models;

namespace TimeAnalyzer.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    public class AuthenticationController : Controller
    {
        private readonly IUserManager userManager;

        public AuthenticationController(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> SignIn(UserLoginModel loginInfo)
        {
            try
            {
                await this.userManager.SignInAsync(this.HttpContext, loginInfo);
                return this.Ok();
            }
            catch(IncorrectLogInInfoException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await this.userManager.Logout(this.HttpContext);
            return this.Ok();
        }
    }
}