using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using Effortless.Net.Encryption;
using System.Security.Cryptography;

namespace EncDecExample.Services
{
    public class EncryptionService : IEncryptionService
    {
       

        public string Encrypt(string plainText,string mySecretKey)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;
            if (string.IsNullOrEmpty(mySecretKey))
            {
                throw new ArgumentException("Secret key cannot be null or empty.", nameof(mySecretKey));
            }

            byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(mySecretKey));
            byte[] iv = Bytes.GenerateIV();

            string encryptedText = Strings.Encrypt(plainText, keyBytes, iv);

            string ivString = Convert.ToBase64String(iv);

            Console.WriteLine($"encryptedText: {encryptedText} + iv: {ivString}");
            return $"{ivString}:{encryptedText}";
        }

        public string Decrypt(string encryptedData, string mySecretKey)
        {
            if (string.IsNullOrEmpty(encryptedData)) return encryptedData;

            byte[] keyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(mySecretKey));

            string[] parts = encryptedData.Split(':');
            Console.WriteLine($"Splitext: {parts}");
            if(parts.Length != 2)
            {
                throw new FormatException("Invalid encrypted text format. Expected format: 'IV:EncryptedText'.");
            }

            byte[] iv = Convert.FromBase64String(parts[0]);
            string encryptedText = parts[1];

            return Strings.Decrypt(encryptedText, keyBytes, iv);
        }
    }
}
