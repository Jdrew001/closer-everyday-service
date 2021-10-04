using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CED.Security
{
    class Salt
    {
        private const int _length = 15;
        public static byte[] GenerateSalt(int? length = null)
        {
            using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length ?? _length];
                rngCsp.GetBytes(randomBytes);
                return randomBytes;
            }
        }
    }
}
