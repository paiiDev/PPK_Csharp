namespace JWT_EncDec_Example.Helpers.EncryptService
{
    public interface IEncryptionService
    {
            string Encrypt(string plainText);
            string Decrypt(string cipherText);
    }
}
