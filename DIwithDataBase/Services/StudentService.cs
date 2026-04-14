using Azure;
using DIwithDataBase.Common;
using DIwithDataBase.DTOs;
using DIwithDataBase.Interfaces;
using DIwithDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIwithDataBase.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repo;
        public StudentService(IStudentRepository repo)
        {
            _repo = repo;
        }

        public async Task<Result<DomainStudentDto>> GetStudent(int id)
        {
            try
            {
                var student = await _repo.GetStudentByIdAsync(id);

                if(student is null)
                {
                    return Result<DomainStudentDto>.Failure("Student not found");
                }

                var dto = new DomainStudentDto
                {
                    Name = student.Name,
                    Age = student.Age,
                };
                return Result<DomainStudentDto>.Success(dto);
            } catch (Exception ex)
            {
                return Result<DomainStudentDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<List<DomainStudentDto>>> GetStudents()
        {
            try
            {
                var students = await _repo.GetAllStudentsAsync();
                if (students is null)
                {
                    return Result<List<DomainStudentDto>>.Failure("No data found");
                }
                var dto = students.Select(x => new DomainStudentDto
                {
                    Name = x.Name,
                    Age = x.Age,
                }).ToList();

                return Result<List<DomainStudentDto>>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<List<DomainStudentDto>>.Failure(ex.Message);
            }
        }

        public async Task<Result<DomainStudentDto>> CreateStudent(Student student)
        {
            try
            {
                var result = _repo.CreateStudent(student);
                if(result is null)
                {
                    return Result<DomainStudentDto>.Failure("Data creation failed");
                }
                var dto = new DomainStudentDto
                {
                    Name = student.Name,
                    Age = student.Age,
                };
                return Result<DomainStudentDto>.Success(dto);
            }
            catch (Exception ex)
            {
                return Result<DomainStudentDto>.Failure(ex.Message);
            }
        }
    }
}
