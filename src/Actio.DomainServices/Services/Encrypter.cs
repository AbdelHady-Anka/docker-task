using System;
using System.Security.Cryptography;

namespace Actio.DomainServices.Services
{
    public class Encrypter : IEncrypter
    {
        private const int SALT_SIZE = 40;
        private const int DERIVE_BYTES_ITERATIONS_COUNT = 10000;
        public string GetHash(string value, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DERIVE_BYTES_ITERATIONS_COUNT);

            return Convert.ToBase64String(pbkdf2.GetBytes(SALT_SIZE));
        }

        public string GetSalt(string value)
        {
            var random = new Random();
            var saltBytes = new byte[SALT_SIZE];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        private byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }


    }
}