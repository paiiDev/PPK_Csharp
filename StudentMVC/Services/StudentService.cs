using Microsoft.EntityFrameworkCore;
using StudentMVC.Common;
using StudentMVC.DataAccess;
using StudentMVC.Models;

namespace StudentMVC.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<StudentResponseDto>>> GetAllStudentsAsync()
        {
            var students = await _context.Students.AsNoTracking().Where(x => !x.DeleteFlag).ToListAsync();
            if (students is null)
            {
                return Result<List<StudentResponseDto>>.Failure("No data found");
            }
            var dto = students.Select(x => new StudentResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Age = x.Age,
            }).ToList();

            return Result<List<StudentResponseDto>>.Success(dto);


        }

        public async Task<Result<bool>> CreateStudentAsync(CreateStudentRequestDto request)
        {
            var student = new Student
            {
                Name = request.Name,
                Age = request.Age,
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }

        public async Task<Result<StudentResponseDto>> GetStudentByIdAsync(int id)
        {
            var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if (student is null)
            {
                return Result<StudentResponseDto>.Failure("Student not found");
            }
            var dto = new StudentResponseDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
            };
            return Result<StudentResponseDto>.Success(dto);
        }

        public async Task<Result<bool>> UpdateStudentAsync(int id, EditStudentRequestDto request)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if (student is null)
            {
                return Result<bool>.Failure("Student not found");
            }
            student.Name = request.Name;
            student.Age = request.Age;
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        public async Task<Result<bool>> DeleteStudentAsync(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if (student is null)
            {
                return Result<bool>.Failure("Student not found");
            }
            student.DeleteFlag = true;
            await _context.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}
