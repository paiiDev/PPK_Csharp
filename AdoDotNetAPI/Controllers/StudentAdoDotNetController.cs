using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetTrainingBatch.AdoDotNetWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAdoDotNetController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentAdoDotNetController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            // Database Connection String ကို appsettings.json ကနေ ယူလိုက်ပါတယ်။
            string connectionString = _configuration.GetConnectionString("DbConnection")!;

            // SqlConnection နဲ့ SqlCommand ကို using statement နဲ့ ကြေညာထားတဲ့ resource တွေကို အလိုအလျောက် dispose လုပ်သွားအောင်ပါ။
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Students WHERE DeleteFlag = 0;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // SqlDataReader က query ကနေပြန်လာတဲ့ data တွေကို တစ်ကြောင်းချင်းစီ ဖတ်ဖို့အတွက်ပါ။
                    SqlDataReader reader = command.ExecuteReader();
                    List<Student> studetns = new List<Student>();

                    while (reader.Read())
                    {
                        // Data reader ကနေဖတ်လို့ရတဲ့ data တွေကို ProductModel ထဲကို ထည့်ပါတယ်။
                        Student student = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                        };
                        studetns.Add(student);
                    }

                    // Connection ကို ပိတ်လိုက်ပါတယ်။
                    connection.Close();
                    return Ok(studetns);
                }
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            string connectionString = _configuration.GetConnectionString("DbConnection")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Students WHERE Id = @Id AND DeleteFlag = 0;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Note: SQL Injection ကို ကာကွယ်ဖို့အတွက် SqlParameter ကို သုံးဖို့ လိုအပ်ပါတယ်။
                    command.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        Student stu = new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = Convert.ToString(reader["Name"]),
                            Age = Convert.ToInt32(reader["Age"]),
                            DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
                        };
                        connection.Close();
                        return Ok(stu);
                    }
                    else
                    {
                        connection.Close();
                        return NotFound("Data not found.");
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult CreateProduct(Student StudentRequestModel)
        {
            string connectionString = _configuration.GetConnectionString("DbConnection")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Student (Name, Age, DeleteFlag) 
                                 VALUES (@Id, @Name, 0)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Request ကနေပါလာတဲ့ Data တွေကို Parameter အနေနဲ့ ထည့်ပေးပါတယ်။
                    command.Parameters.AddWithValue("@Id", StudentRequestModel.Id);
                    command.Parameters.AddWithValue("@Name", StudentRequestModel.Name);

                    // ExecuteNonQuery() က INSERT, UPDATE, DELETE query တွေအတွက်သုံးပြီး affected rows အရေအတွက်ကို return ပြန်ပေးပါတယ်။
                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    return Ok(result > 0 ? "Saving Successful." : "Saving Failed.");
                }
            }
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateProduct(int id, StudentRequestModel requestModel)
        {
            string connectionString = _configuration.GetConnectionString("DbConnection")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"UPDATE Students
                                 SET Name = @Name, 
                                     Age = @Age, 
                                 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Name", requestModel.Name);
                    command.Parameters.AddWithValue("@Age", requestModel.Age);

                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    return Ok(result > 0 ? "Updating Successful." : "Updating Failed.");
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            string connectionString = _configuration.GetConnectionString("DbConnection")!;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // ဒါက soft delete ပါ။ တကယ်မဖျက်ဘဲ DeleteFlag ကိုပဲ 1 ပြောင်းလိုက်တာပါ။
                string query = "UPDATE Students SET DeleteFlag = 1 WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    return Ok(result > 0 ? "Deleting Successful." : "Deleting Failed.");
                }
            }
        }
    }
}