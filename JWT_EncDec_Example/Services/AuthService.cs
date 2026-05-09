using JWT_EncDec_Example.DTOs;
using JWT_EncDec_Example.DummyData;

namespace JWT_EncDec_Example.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        public AuthService(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }
        public AdminLoginResponseDto AuthenticateAdmin(LoginAdminRequestDto request)
        {
            var admin = AdminsDummyData.Admins.FirstOrDefault(a => a.UserName == request.UserName && a.Password == request.Password);
            // Simulate admin authentication (replace with real authentication logic)
            if (admin is null)
            {
                return new AdminLoginResponseDto { IsSuccessful = false, Message = "Ivalid username or password" };

            }
              var token = _tokenService.GenerateToken(admin.Id, admin.UserName, admin.Role);
                return new AdminLoginResponseDto
                {
                    UserName = admin.UserName,
                    Role = admin.Role,
                    Token = token
                };

            
        }
    }

}
