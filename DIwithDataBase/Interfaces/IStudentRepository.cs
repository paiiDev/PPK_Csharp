using DIwithDataBase.Common;
using DIwithDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIwithDataBase.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetStudentByIdAsync(int id);
        Task<List<Student>> GetAllStudentsAsync();
    }
}
