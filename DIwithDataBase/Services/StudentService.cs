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

        public async Task<Result<DomainStudentDto>> CreateStudent(DomainStudentDto studentInput)
        {
            try
            {
                var student = new Student
                {
                    Name = studentInput.Name,
                    Age = studentInput.Age,
                };

                var result = await _repo.CreateStudentAsync(student);
                if(result is null)
                {
                    return Result<DomainStudentDto>.Failure("Data creation failed");
                }

                var dto = new DomainStudentDto
                {
                    Id  = student.Id,
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

        public async Task<Result<DomainStudentDto>> UpdateStudent(DomainStudentDto student)
        {
            try
            {
                var existingStudent = await _repo.GetStudentByIdAsync(student.Id);
                if (existingStudent is null)
                {
                    return Result<DomainStudentDto>.Failure("Student not found");
                }

         

                existingStudent.Name = student.Name;
                existingStudent.Age = student.Age;

                await _repo.UpdateStudentAsync(existingStudent);
                
                var dto = new DomainStudentDto
                {
                    Id = existingStudent.Id,
                    Name = existingStudent.Name,
                    Age = existingStudent.Age,
                };

                return Result<DomainStudentDto>.Success(dto);

            }
            catch (Exception ex)
            {
                return Result<DomainStudentDto>.Failure(ex.Message);
            }
        }

        public async Task<Result<bool>> DeleteStudent(int id)
        {
            try
            {
                var existingStudent = await _repo.GetStudentByIdAsync(id);
                if (existingStudent is null)
                {
                    return Result<bool>.Failure("Student not found");
                }
                var result = await _repo.DeleteStudentAsync(existingStudent);
                if (!result)
                {
                    return Result<bool>.Failure("Failed to delete student");
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
        }
        }
}
