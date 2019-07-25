using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AccessControl.Model;

namespace AccessControl.Authentication.Security
{
    public class EncryptionSecuredPassword : ISecuredPassword
    {
        private static readonly int SALT_SIZE = 16;

        private static readonly int HASH_SIZE = 20;

        private static readonly int ITERATIONS = 10000;

        private static byte[] CreateSalt(RandomNumberGenerator rng, int size)
        {
            byte[] bytes = new byte[size];
            rng.GetBytes(bytes);
            return bytes;
        }

        public HashedPassword Create(string password)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] salt = CreateSalt(rng, SALT_SIZE);
            return Restore(password, salt);
        }

        public bool Verify(string test, HashedPassword password)
        {
            HashedPassword testConverted = Restore(test, password.Salt);

            bool mismatched = false;

            int size = HASH_SIZE;

            byte[] hashedPassword = password.Value;

            byte[] testPassword = testConverted.Value;

            for(int i = 0; i < size; ++i)
            {
                if(testPassword[i] != hashedPassword[i])
                {
                    mismatched = true;
                    break;
                }
            }

            return mismatched;
        }

        private HashedPassword Restore(string password, byte[] salt)
        {
            DeriveBytes encryptor = new Rfc2898DeriveBytes(password, salt, ITERATIONS);

            byte[] hash = encryptor.GetBytes(HASH_SIZE);

            byte[] hashedPassword = new byte[SALT_SIZE + HASH_SIZE];

            Array.Copy(salt, 0, hashedPassword, 0, SALT_SIZE);

            Array.Copy(hash, SALT_SIZE, hashedPassword, SALT_SIZE, HASH_SIZE);

            return new HashedPassword(salt, hashedPassword);
        }
    }
}
