namespace Dapper.Features.Students
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> ReadAllData();
        Task<Student> ReadById(int id);
        Task<int> Create(Student student);
        Task<int> Update(int id, Student student);
        Task<int> Delete(int id);
  }
}
