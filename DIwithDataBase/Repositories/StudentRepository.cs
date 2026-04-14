using DIwithDataBase.Common;
using DIwithDataBase.DataAccess;
using DIwithDataBase.Interfaces;
using DIwithDataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIwithDataBase.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Student> GetStudentByIdAsync(int id)
        {     
              return await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.AsNoTracking().Where(x => !x.DeleteFlag).ToListAsync();
        }
        
        public async Task<bool> CreateStudent(Student student)
        {
            using var context = _context;
            var stu = new Student
            {
                Name = student.Name,
                Age = student.Age,

            };
            var result = context.Students.Add(stu);
            context.SaveChanges();
            return true;
        }
    }
}
