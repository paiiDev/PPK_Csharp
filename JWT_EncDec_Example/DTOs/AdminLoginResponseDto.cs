namespace JWT_EncDec_Example.DTOs
{
    public class AdminLoginResponseDto
    {
        public string UserName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public bool IsSuccessful { get; set; } = true;
        public string Message { get; set; } = "Success Login";
    }
}
