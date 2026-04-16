using DIwithDataBase.Common;
using DIwithDataBase.DTOs;
using DIwithDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIwithDataBase.Interfaces
{
    public interface IStudentService
    {
        Task<Result<DomainStudentDto>> GetStudent(int id);
        Task<Result<List<DomainStudentDto>>> GetStudents();

        Task<Result<DomainStudentDto>> CreateStudent(DomainStudentDto student);
    }
}
