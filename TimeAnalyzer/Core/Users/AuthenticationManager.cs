using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TimeAnalyzer.Core.Exceptions;
using TimeAnalyzer.Core.Interfaces;
using TimeAnalyzer.Core.Static;
using TimeAnalyzer.Domain.Interfaces;
using TimeAnalyzer.Domain.Models.Users;
using TimeAnalyzer.Models;

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
            User user = await GetApprovedUser(userLoginInfo);

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
            User user = await userRepository.GetByEmail(userLoginInfo.Email);

            if (user != null && UserPasswordIsValid(user, userLoginInfo))
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
