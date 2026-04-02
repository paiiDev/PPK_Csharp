using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DapperStudentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DapperStudentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetAllData()
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                var Query = $@"SELECT [Id]
                 ,[Name]
                 ,[Age]
                  FROM [dbo].[Students]";

                var students = db.Query<Student>(Query).ToList();
                return Ok(students);
            }

        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                var Query = $@"SELECT [Id]
                 ,[Name]
                 ,[Age]
                  FROM [dbo].[Students] WHERE Id = @id";
                var student = db.QueryFirstOrDefault<Student>(Query, new { id });
                if (student is null)
                {
                    return NotFound("No data found.");
                }
                return Ok(student);
            }

        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                var Query = $@"INSERT INTO [dbo].[Students]
                               ([Name]
                               ,[Age])
                                VALUES
                               (@name,
                               @age)";
                var result = db.Execute(Query, new { name = student.Name, age = student.Age });
                return Ok(result > 0 ? "Data inserted successfully." : "Data insertion failed.");
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, Student student)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                var Query = $@"UPDATE [dbo].[Students]
                               SET Name = @name,
                                   Age = @age
                               WHERE Id = @id";
                var result = db.Execute(Query, new { name = student.Name, age = student.Age, id });
                return Ok(result > 0 ? "Data updated successfully." : "Data updation failed.");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(_configuration.GetConnectionString("DbConnection")))
            {
                var Query = $@"UPDATE [dbo].[Students]
                               SET DeleteFlag = 1
                               WHERE Id = @id";
                var result = db.Execute(Query, new { id });
                return Ok(result > 0 ? "Data deleted successfully." : "Data deletion failed.");
            }
        }

    }
}