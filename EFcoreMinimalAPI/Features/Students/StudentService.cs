
using EFcoreMinimalAPI;
using EFcoreMinimalAPI.Features.Students;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EFcoreMinimalAPI.Features.Students
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        public StudentService(AppDbContext context)
        {
            _context = context;
        }

       
        public async Task<IEnumerable<Student>> ReadAllData()
        {
            return await _context.Students.AsNoTracking().ToListAsync();
           
        }
        public async Task<Student> ReadById(int id)
        {
            return await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<int> Create(Student student)
        {
             _context.Students.Add(student);
            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(int id, Student student)
        {
           var existing = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) {
                return 0;
            }
            existing.Name = student.Name;
            existing.Age = student.Age;
            _context.Students.Add(student);
            return await _context.SaveChangesAsync();

        }
        public async Task<int> Delete(int id)
        {
           var existing = await _context.Students.FirstOrDefaultAsync( x => x.Id == id);
            if (existing is null)
            {
                return 0;
            }
            _context.Students.Remove(existing);
            return await _context.SaveChangesAsync();
        }
    }
}
