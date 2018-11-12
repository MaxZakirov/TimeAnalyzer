using TimeAnalyzer.Domain.Models.Users;
using System;
using TimeAnalyzer.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TimeAnalyzer.Core.Static;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace TimeAnalyzer.Core.Users
{
    public class UserManager
    {
        private readonly ICredentialTypesRepository credentialTypesRepository;
        private readonly ICredentialsRepository credentialsRepository;
        private readonly IUsersRepository usersRepository;

        public UserManager(
            ICredentialTypesRepository credentialTypesRepository,
            ICredentialsRepository credentialsRepository,
            IUsersRepository usersRepository)
        {
            this.credentialTypesRepository = credentialTypesRepository;
            this.credentialsRepository = credentialsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<User> ValidateAsync(string loginTypeCode, string identifier, string secret)
        {
            CredentialType credentialType = await this.credentialTypesRepository.GetByCodeAsync(loginTypeCode);

            if (credentialType == null)
            {
                return null;
            }

            IEnumerable<Credential> credentials = await this.credentialsRepository.GetCredentialsByTypeAsync(credentialType.Id);
            Credential credential = credentials.FirstOrDefault(c => string.Equals(c.Identifier == identifier, StringComparison.OrdinalIgnoreCase) && c.Secret == MD5Hasher.CalculateHash(secret));

            if (credential == null)
                return null;

            return await usersRepository.GetById(credential.UserId);
        }

        public async void SignInAsync(HttpContext httpContext, User user, bool isPersistent = false)
        {
            ClaimsIdentity identity = this.GetUserIdentity(user);

            DateTime currentTime = DateTime.Now;
            JwtSecurityToken jwt = new JwtSecurityToken(
                    issuer: AuthOptions.Issuer,
                    audience: AuthOptions.Audeince,
                    notBefore: currentTime,
                    claims: identity.Claims,
                    expires: currentTime.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties() { IsPersistent = isPersistent }
                );
        }

        private ClaimsIdentity GetUserIdentity(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };
            claims.AddRange(this.GetUserRoleClaims());
            ClaimsIdentity identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return identity;
        }

        private IEnumerable<Claim> GetUserRoleClaims()
        {
            return Enumerable.Empty<Claim>();
        }
    }
}
