using System.Security.Cryptography;
using System.Text;

namespace Users.Microservice.Services.Extentions
{
    public static class StringExtention
    {
        public static string Encrypt(this string password)
        {
            using (SHA256 sha256HASH = SHA256.Create())
            {
                var hashedBytes = sha256HASH.ComputeHash(Encoding.UTF8.GetBytes(password));

                var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hashedPassword;
            }
        }
    }
}
