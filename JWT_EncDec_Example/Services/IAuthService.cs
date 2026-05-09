using JWT_EncDec_Example.DTOs;

namespace JWT_EncDec_Example.Services
{
    public interface IAuthService
    {
        AdminLoginResponseDto AuthenticateAdmin(LoginAdminRequestDto request);
    }
}
