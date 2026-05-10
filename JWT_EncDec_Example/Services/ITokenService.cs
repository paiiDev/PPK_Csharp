namespace JWT_EncDec_Example.Services
{
    public interface ITokenService
    {
        // Include role so generated JWT contains a role claim that Authorize(Roles="...") can evaluate
        string GenerateToken(int userId, string userName, string role);
    }
}
