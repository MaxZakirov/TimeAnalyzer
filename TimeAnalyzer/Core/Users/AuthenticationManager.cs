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
using Microsoft.Extensions.Options;

namespace TimeAnalyzer.Core.Users
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly IUserRepository userRepository;
        private readonly AppSettings appSettings;

        public AuthenticationManager(
            IUserRepository userRepository,
            IOptions<AppSettings> appSettings
            )
        {
            this.userRepository = userRepository;
            this.appSettings = appSettings.Value;
        }

        public async Task<User> Authenticate(UserLoginModel userLoginInfo)
        {
            User user = await this.GetApprovedUser(userLoginInfo);

            if (user == null)
            {
                throw new IncorrectLogInInfoException("The credentials are invalid. Try again");
            }

            user.Token = GenerateNewToken(user.Name);
            user.Password = null;
            return user;
        }

        public User CheckIn(UserCheckinModel user)
        {
            string cryptedPassword = MD5Hasher.CalculateHash(user.Password);
            User newUser = new User(user.Name, user.Email, cryptedPassword);
            newUser.Id = userRepository.Add(newUser);

            newUser.Token = GenerateNewToken(newUser.Name);
            newUser.Password = null;
            return newUser;
        }

        public async Task Logout(HttpContext httpContext)
        {
            await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private string GenerateNewToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userName.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<User> GetApprovedUser(UserLoginModel userLoginInfo)
        {
            User user = await this.userRepository.GetByEmail(userLoginInfo.Email);

            if (user != null && this.UserPasswordIsValid(user, userLoginInfo))
            {
                return user;
            }

            return null;
        }

        private bool UserPasswordIsValid(User realUser, UserLoginModel loginInfo)
        {
            string userPasswordHash = MD5Hasher.CalculateHash(loginInfo.Password);
            return realUser.Password == userPasswordHash;
        }
    }
}
