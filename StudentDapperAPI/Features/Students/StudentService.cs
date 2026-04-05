using Dapper;
using Microsoft.Data.SqlClient;
using StudentDapperAPI.Common;
using System.Data;

namespace StudentDapperAPI.Features.Students
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString;
        public StudentService(IConfiguration config)
        {
          _connectionString = config.GetConnectionString("DbConnection");
        }

        private IDbConnection CreateConnection() =>  new SqlConnection(_connectionString);

        public async Task<Result<List<Student>>> ReadAllData()
        {
            try
            {
                using var db = CreateConnection();
                var data = await db.QueryAsync<Student>("SELECT * FROM Students");
                return Result<List<Student>>.Success(data.ToList());
            }
            catch (Exception ex)
            {
                return Result<List<Student>>.Failure(ex.Message);
            }
          

        }
        public async Task<Result<Student>> ReadById(int id)
        {
            try
            {
                using var db = CreateConnection();
                var data = await db.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE Id = @id", new { Id = id });
                if (data is null)
                {
                    return Result<Student>.Failure("Data not found");
                }
                return Result<Student>.Success(data);
            }
            catch (Exception ex)
            {
                return Result<Student>.Failure(ex.Message);
            }
           
        }
        public async Task<Result<bool>> Create(Student student)
        {
            try
            {
                using var db = CreateConnection();
                var item = await db.ExecuteAsync("INSERT INTO Students (Name, Age, DeleteFlag) VALUES (@Name, @Age, @DeleteFlag)", student);
                return Result<bool>.Success(item > 0);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
           
        }
        public async Task<Result<bool>> Update(int id, Student student)
        {
            try
            {
                using var db = CreateConnection();
                string query = @"UPDATE Students 
                           SET Name = @Name, Age = @Age 
                           WHERE Id = @Id";

                var item = await db.ExecuteAsync(query, new { name = student.Name, age = student.Age, id });
                return Result<bool>.Success(item > 0);
            } catch(Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }
          
        }
        public async Task<Result<bool>> Delete(int id)
        {
            try
            {
                using var db = CreateConnection();

                var item = await db.ExecuteAsync("DELETE FROM Students WHERE Id = @Id", new { Id = id });
                return Result<bool>.Success(item > 0);
            } catch (Exception ex)
            {
                return Result<bool>.Failure(ex.Message);
            }

        }
    }
}
