using Microsoft.EntityFrameworkCore;
using StudentMVC.Common;
using StudentMVC.DataAccess;
using StudentMVC.Models;

namespace StudentMVC.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService (AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<StudentResponseDto>>> GetAllStudentsAsync()
        {
            var students = await  _context.Students.AsNoTracking().Where(x => !x.DeleteFlag).ToListAsync();
            if (students is null)
            {
                return Result<List<StudentResponseDto>>.Failure("No data found");
            }
            var dto =  students.Select(x => new StudentResponseDto
            {
                Name = x.Name,
                Age = x.Age,
            }).ToList();

            return Result<List<StudentResponseDto>>.Success(dto);


        }
    }
}
