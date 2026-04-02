using Microsoft.Data.SqlClient;

namespace StudentAPI.Services
{
    public class StudentService
    {
        private readonly string _connectionString;
        public StudentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        public List<Student> GetStudents()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                List<Student> students = new List<Student>();
                conn.Open();

                string Query = "SELECT Id, Name, Age FROM Students WHERE DeleteFlag = 0";
                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    students.Add(new Student
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"])
                    });
                }
                return (students);
            }
        }

        public Student GetStudent(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                Student student = null;

                conn.Open();

                string Query = "SELECT Id, Name, Age FROM Students WHERE Id = @Id AND DeleteFlag = 0";
                SqlCommand cmd = new SqlCommand(Query, conn);

                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    student = new Student
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Name = reader["Name"].ToString(),
                        Age = Convert.ToInt32(reader["Age"])
                    };
                }
                return (student);
            }
        }

        public void Create(Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = "INSERT INTO Students (Name, Age) VALUES (@Name, @Age)";
                SqlCommand cmd = new SqlCommand(Query, conn);

                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                cmd.ExecuteNonQuery();
            }
        }

        public void Update(int id, Student student)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id AND DeleteFlag = 0";
                SqlCommand cmd = new SqlCommand(Query, conn);

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", student.Name);
                cmd.Parameters.AddWithValue("@Age", student.Age);

                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = "UPDATE Students SET DeleteFlag = 1 WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(Query, conn);

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();
            }
        }
        }

}