using Microsoft.VisualBasic;
using System.Text;
using Effortless.Net.Encryption;

namespace JWT_EncDec_Example.Helpers.EncryptService
{
    public class EncryptService : IEncryptionService
    {
        private readonly IConfiguration config;
        public EncryptService(IConfiguration config)
        {
            this.config = config;
        }

        public string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;
            var encryptionKey = config["EncryptionSettings:Key"];
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentException("Secret key cannot be null or empty.", "EncryptionSettings:Key");
            }

            byte[] keyBytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(encryptionKey));
            byte[] iv = Bytes.GenerateIV();

            string encryptedText = Effortless.Net.Encryption.Strings.Encrypt(plainText, keyBytes, iv);

            string ivString = Convert.ToBase64String(iv);

            return $"{ivString}:{encryptedText}";
        }

        public string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            var encryptionKey = config["EncryptionSettings:Key"];
            if (string.IsNullOrEmpty(encryptionKey))
            {
                throw new ArgumentException("Secret key cannot be null or empty.", "EncryptionSettings:Key");
            }
            string[] parts = cipherText.Split(':', 2);
            if (parts.Length != 2)
            {
                throw new FormatException("Invalid cipher text format.");
            }
            byte[] iv = Convert.FromBase64String(parts[0]);
            string encryptedText = parts[1];
            byte[] keyBytes = System.Security.Cryptography.SHA256.HashData(Encoding.UTF8.GetBytes(encryptionKey));
            return Effortless.Net.Encryption.Strings.Decrypt(encryptedText, keyBytes, iv);
        }
    }
}
