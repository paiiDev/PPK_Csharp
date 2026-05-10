using JWT_EncDec_Example.DTOs;

namespace JWT_EncDec_Example.Services.Student
{
    public interface IStudentService
    {
        StudentResponseDto CreateStudent(StudentRequestDto request);
        List<StudentResponseDto> GetStudents();
    }
}
