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
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TimeAnalyzer.Core.Users
{
    public class UserManager : IUserManager
    {
        private readonly IUsersRepository userRepository;
        private readonly AppSettings appSettings;

        public UserManager(
            IUsersRepository userRepository,
            AppSettings appSettings
            )
        {
            this.userRepository = userRepository;
            this.appSettings = appSettings;
        }

        public async Task<User> Authenticate(HttpContext httpContext, UserLoginModel userLoginInfo)
        {
            User user = await this.GetApprovedUser(userLoginInfo);

            if(user==null)
            {
                throw new IncorrectLogInInfoException("The credentials are invalid. Try again");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.PasswordHash = null;

            return user;
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private async Task<User> GetApprovedUser(UserLoginModel userLoginInfo)
        {
            User user = await this.userRepository.GetByEmail(userLoginInfo.Email);

            if(user!=null && this.UserPasswordIsValid(user,userLoginInfo))
            {
                return user;
            }

            return null;
        }

        private bool UserPasswordIsValid(User realUser, UserLoginModel loginInfo)
        {
            string userPasswordHash = MD5Hasher.CalculateHash(loginInfo.Password);
            return realUser.PasswordHash == userPasswordHash;
        }
    }
}
