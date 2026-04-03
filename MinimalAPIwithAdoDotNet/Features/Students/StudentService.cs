using Microsoft.Data.SqlClient;

namespace MinimalAPIwithAdoDotNet.Features.Students
{
    public class StudentService
    {
        private readonly string _connectionString;
        public StudentService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
        }

        private SqlConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<List<Student>> ReadAll()
        {
            var list = new List<Student>();

            using var conn = CreateConnection();
            await conn.OpenAsync();

            string Query = "SELECT Id, Name, Age FROM Students WHERE DeleteFlag = 0";
            using var cmd = new SqlCommand(Query, conn);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new Student
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["Name"].ToString(),
                    Age = Convert.ToInt32(reader["Age"])
                });
            }
            return list;

        
        }

        public async Task<Student> Read(int id)
        {
            using var conn = CreateConnection();
            conn.OpenAsync();

            string Query = "SELECT Id, Name, Age FROM Students WHERE Id = @Id AND DeleteFlag = 0";
            using var cmd = new SqlCommand(Query, conn);

            cmd.Parameters.AddWithValue("@Id", id);
            using var reader = await cmd.ExecuteReaderAsync();

            if(await reader.ReadAsync())
            {
                return (new Student
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["Name"].ToString(),
                    Age = Convert.ToInt32(reader["AGe"])
                });
            }

            return null;
            
           
        }

        public async Task<int> Create (Student student)
        {
            using var conn = CreateConnection();
            conn.OpenAsync();

            String Query = "INSERT INTO Students (Name, Age) VALUES (@Name, @Age)";

            using var cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Age", student.Age);

             return await cmd.ExecuteNonQueryAsync();
           
        }

        public async Task<int> Update (int id, Student student)
        {
            using var conn = CreateConnection();
            conn.OpenAsync();

            String Query = "UPDATE Students SET Name = @Name, Age = @Age WHERE Id = @Id AND DeleteFlag = 0";

            using var cmd = new SqlCommand(Query, conn);
            cmd.Parameters.AddWithValue("@Name", student.Name);
            cmd.Parameters.AddWithValue("@Age", student.Age);
            cmd.Parameters.AddWithValue("Id", id);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> Delete (int id)
        {
            using var conn = CreateConnection();
            conn.OpenAsync();

            String Query = "UPDATE Students SET DeleteFlag = 1 WHERE Id = @Id";

            using var cmd = new SqlCommand (Query, conn);
            cmd.Parameters.AddWithValue("@Id", id);

            return await cmd.ExecuteNonQueryAsync();


        }




    }
}

