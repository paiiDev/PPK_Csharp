using StudentMVCajax.Common;
using StudentMVCajax.Models;

namespace StudentMVCajax.Services
{
    public interface IStudentService
    {
        Task<Result<List<StudentResponseDto>>> GetAllStudentsAsync();

        Task<Result<bool>> CreateStudentAsync(CreateStudentRequestDto request);

        Task<Result<StudentResponseDto>> GetStudentByIdAsync(int id);

        Task<Result<bool>> UpdateStudentAsync(int id, EditStudentRequestDto request);

        Task<Result<bool>> DeleteStudentAsync(int id);
    }
}
