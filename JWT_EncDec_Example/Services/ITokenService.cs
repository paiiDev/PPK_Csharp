namespace JWT_EncDec_Example.Services
{
    public interface ITokenService
    {
        string GenerateToken(int userId, string userName, string role);

    }
}
