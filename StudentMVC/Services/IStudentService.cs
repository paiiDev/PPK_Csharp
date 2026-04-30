using StudentMVC.Common;
using StudentMVC.Models;

namespace StudentMVC.Services
{
    public interface IStudentService
    {
        Task<Result<List<StudentResponseDto>>> GetAllStudentsAsync();
    }
}
