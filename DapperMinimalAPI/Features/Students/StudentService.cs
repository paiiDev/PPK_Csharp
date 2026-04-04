using Dapper;
using Dapper.Features.Students;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperMinimalAPI.Features.Students
{
    public class StudentService : IStudentService
    {
        private readonly string _connectionString;
        public StudentService(IConfiguration config)
        {
          _connectionString = config.GetConnectionString("DbConnection");
        }

        private IDbConnection CreateConnection() =>  new SqlConnection(_connectionString);

        public async Task<IEnumerable<Student>> ReadAllData()
        {
            using var db =  CreateConnection();
            return await db.QueryAsync<Student>("SELECT * FROM Students");
           
        }
        public async Task<Student> ReadById(int id)
        {
            using var db = CreateConnection();
            return await db.QueryFirstOrDefaultAsync<Student>("SELECT * FROM Students WHERE Id = @id", new {  Id = id});
        }
        public async Task<int> Create(Student student)
        {
            using var db = CreateConnection();
            return await db.ExecuteAsync("INSERT INTO Students (Name, Age, DeleteFlag) VALUES (@Name, @Age, @DeleteFlag)", student);
        }
        public async Task<int> Update(int id, Student student)
        {
            using var db = CreateConnection();
            string query = @"UPDATE Students 
                           SET Name = @Name, Age = @Age 
                           WHERE Id = @Id";

            return await db.ExecuteAsync(query, student);
        }
        public async Task<int> Delete(int id)
        {
            using var db = CreateConnection();

            return await db.ExecuteAsync(
                "DELETE FROM Students WHERE Id = @Id",
                new { Id = id });
        }
    }
}
