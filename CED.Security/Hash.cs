using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CED.Security
{
    public class Hash
    {
        private const int _length = 15;

        public static HashWithSalt GetHash(string password, string salt = null, int? length = null, HashAlgorithm hashAlgo = null)
        {

            byte[] saltBytes = !string.IsNullOrEmpty(salt) ? Convert.FromBase64String(salt) : Salt.GenerateSalt(length ?? _length);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            if (hashAlgo == null)
                hashAlgo = new SHA512CryptoServiceProvider();
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSalt
            {
                Salt = Convert.ToBase64String(saltBytes),
                Hash = Convert.ToBase64String(digestBytes)
            };
        }
    }
}
