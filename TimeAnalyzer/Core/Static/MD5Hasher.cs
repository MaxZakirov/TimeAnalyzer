using System.Security.Cryptography;
using System.Text;

namespace TimeAnalyzer.Core.Static
{
    public static class MD5Hasher
    {
        public static string CalculateHash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.UTF32.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                stringBuilder.Append(hash[i].ToString("X2"));
            }

            return stringBuilder.ToString();
        }
    }
}
