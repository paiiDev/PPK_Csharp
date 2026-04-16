using DIwithDataBase.Common;
using DIwithDataBase.DataAccess;
using DIwithDataBase.DTOs;
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
        
        public async Task<Student> CreateStudent(Student student)
        {
            
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return student;
        }
    }
}
