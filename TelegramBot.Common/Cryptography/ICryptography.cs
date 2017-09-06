using System.IO;
namespace TelegramBot.Common.Cryptography{
    public interface ICryptographer{
        string CreateSalt();
        string ComputeHash(string valueToHash);
        string GetPasswordHash(string password, string salt);
        byte[] Encrypt(byte[] data, byte[] key = null);
        string Encrypt(string data, byte[] key = null);
        byte[] Decrypt(byte[] data, byte[] key = null);
        string Decrypt(string data, byte[] key = null);
        string GetHashFromKeyAndSalt(string key, string salt);


    }
}