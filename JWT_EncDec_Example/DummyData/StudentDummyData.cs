using JWT_EncDec_Example.DTOs;

namespace JWT_EncDec_Example.DummyData
{
    public static class StudentDummyData
    {
        public static List<StudentResponseDto> Students { get; } = new List<StudentResponseDto>
        {
          new StudentResponseDto { Id = 1, Name = "John Doe", PhoneNumber = "1234567890", Email = "johndoe@email.com", Nrc = "12/ABC(N)123456" },
          new StudentResponseDto { Id = 2, Name = "Jane Smith", PhoneNumber = "0987654321", Email = "janesmit.com", Nrc = "34/DEF(N)654321" },
        };
    }
}
