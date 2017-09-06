using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TelegramBot.Common.Cryptography
{
    public class Cryptographer : ICryptographer
    {
        // Rijndael Key of 16-bytes

        private static readonly byte[] Key = { 45, 7, 124, 55, 34, 16, 14, 156, 33, 8, 99, 61, 226, 95, 68, 36 };

        public string ComputeHash(string valueToHash)
        {
            HashAlgorithm algorithm = SHA256.Create();
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(valueToHash));

            return Convert.ToBase64String(hash);
        }

        public string CreateSalt()
        {
            var size = 64;
            //Generate a cryptographic random number.
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
        public byte[] Decrypt(byte[] data, byte[] key = null){
            key = key ?? Key;
            try
            {

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();

                        return ms.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Decrypt(string data, byte[] key = null)
        {
            key = key ?? Key;
            try
            {
                // byte data
                var internalData = Convert.FromBase64String(data);

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();

                        return Encoding.ASCII.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public byte[] Encrypt(byte[] data, byte[] key = null){
            key = key ?? Key;
            try
            {
                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();

                        return ms.ToArray();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Encrypt(string data, byte[] key = null)
        {
            key = key ?? Key;
            try
            {
                // byte data
                var internalData = Encoding.ASCII.GetBytes(data);

                // Create a new Rijndael object to generate a key
                // and initialization vector (IV).
                var rijndaelAlgo = new RijndaelManaged { Key = key, Padding = PaddingMode.PKCS7 };
                rijndaelAlgo.IV = new byte[rijndaelAlgo.IV.Length];

                // Create a CryptoStream using the MemoryStream
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, rijndaelAlgo.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(internalData, 0, internalData.Length);
                        cs.FlushFinalBlock();

                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetHashFromKeyAndSalt(string key, string salt)
        {
            return GetPasswordHash(key, salt);
        }

        public string GetPasswordHash(string password, string salt)
        {
            return ComputeHash(password + salt);
        }
    }
}
