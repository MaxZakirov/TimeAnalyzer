using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeAnalyzer.Core
{
    public static class AuthOptions
    {
        public static string Issuer = "TimeAnalyzerApp";
        public static string Audeince = @"http://localhost:54953/";
        private static string key = "secret_key_1234";
        public static int Lifetime = 20;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
