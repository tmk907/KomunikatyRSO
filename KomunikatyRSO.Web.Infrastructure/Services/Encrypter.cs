using System;
using System.Security.Cryptography;
using System.Text;

namespace KomunikatyRSO.Web.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        public string GetHash(string value)
        {
            var crypt = new SHA256Managed();
            var hash = new StringBuilder();
            byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(value));
            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }
            return hash.ToString();
        }

        public string GetToken(Guid userId, string passwordHash)
        {
            return GetHash(userId.ToString() + passwordHash);
        }
    }
}
