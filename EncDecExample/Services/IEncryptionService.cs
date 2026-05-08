using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncDecExample.Services
{
    public interface IEncryptionService
    {
        string Encrypt(string plainText,string mySecretKey);
        string Decrypt(string cipherText, string mySecretKey);
    }
}
