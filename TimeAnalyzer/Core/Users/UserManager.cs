using TimeAnalyzer.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Models;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeAnalyzer.Domain.Models.Users;
using System;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Core.Exceptions;

namespace TimeAnalyzer.Core.Users
{
    public class UserManager : IUserManager
    {
        private readonly IUsersRepository userRepository;

        public UserManager(
            IUsersRepository userRepository
            )
        {
            this.userRepository = userRepository;
        }

        public async Task SignInAsync(HttpContext httpContext, UserLoginModel userLoginInfo)
        {
            User user = await this.GetApprovedUser(userLoginInfo);

            if(user==null)
            {
                throw new IncorrectLogInInfoException("The credentials are invalid. Try again");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim("Email",user.Email)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task<User> GetApprovedUser(UserLoginModel userLoginInfo)
        {
            User user = await this.userRepository.GetByEmail(userLoginInfo.Email);

            if(user!=null && this.UserCredentialsAreValid(user,userLoginInfo))
            {
                return user;
            }

            return null;
        }

        private bool UserCredentialsAreValid(User realUser, UserLoginModel loginInfo)
        {
            string userPasswordHash = MD5Hasher.CalculateHash(loginInfo.Password);
            return realUser.Name == loginInfo.Email && realUser.PasswordHash == userPasswordHash;
        }
    }
}
