using StudentDapperAPI.Common;

namespace StudentDapperAPI.Features.Students
{
    public interface IStudentService
    {
        Task<Result<List<Student>>> ReadAllData();
        Task<Result<Student>> ReadById(int id);
        Task<Result<bool>> Create(Student student);
        Task<Result<bool>> Update(int id, Student student);
        Task<Result<bool>> Delete(int id);
  }
}
