namespace JWT_EncDec_Example.DTOs
{
    public class StudentRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Nrc { get; set; } = string.Empty;
    }
}
