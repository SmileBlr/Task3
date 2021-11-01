using System;
using System.Text;
using System.Security.Cryptography;

namespace Task3
{
    static class Secure
    {
        private static string sKey;
        private static byte[] bKey = new byte[16];
        public static string Key => sKey;

        public static void GenerateKey()
        {
            var generator = RandomNumberGenerator.Create();
            generator.GetBytes(bKey);
            sKey = BitConverter.ToString(bKey).Replace("-", "");
        }

        public static string GenerateHMAC(string move)
        {
            var hmacsha256 = new HMACSHA256(bKey);
            var hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(move));
            var HMAC = BitConverter.ToString(hash).Replace("-", "");
            
            return HMAC;
        }
    }
}
