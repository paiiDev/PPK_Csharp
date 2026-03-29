using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public class Student
    {
        private readonly string _connectionString;
        public Student(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(string name, int age)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = "INSERT INTO [dbo].[Students]" +
                    "([Name]" +
                    ",[Age]) VALUES " +
                    "(@Name,@Age) ";
                SqlCommand cmd = new SqlCommand(Query, conn);

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.Add("@age", System.Data.SqlDbType.Int).Value = age;

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data inserted successfully.");
                } else
                {
                    Console.WriteLine("Data insertion failed.");
                }

            }
        }

        public void ReadAllData()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = $@"SELECT [Id]
                ,[Name]
                ,[Age]
                 FROM [dbo].[Students]";

                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Console.WriteLine("Name:" + rdr["name"] + " " + "Age: " + rdr["Age"] );
                        ;
                    }
                } else
                {
                    Console.WriteLine("No data found.");
                }
               
            }
        }

        public void Read(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = "SELECT * FROM Students where id = @id";

                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.Write("Name: " + reader["Name"] + " " + "Age: " + reader["Age"]);
                    }

                } else
                {
                    Console.Write("No data found.");
                }
            }
        }

        public void Update(int id, string name, int age)
        {
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = $@"UPDATE [dbo].[Students]
                                   SET [Name] = @name,
                                      [Age] = @age
                                 WHERE id = @id";

                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);

                int rowsAffected = cmd.ExecuteNonQuery();
                if(rowsAffected > 0)
                {
                    Console.WriteLine("Data updated successfully.");
                } else
                {
                    Console.WriteLine("Data update failed.");
                }

            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string Query = "DELETE FROM Students WHERE id = @id";

                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@id",System.Data.SqlDbType.Int).Value = id;

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine("Data deleted successfully.");
                } else
                {
                    Console.WriteLine("Failed to delete data.");
                }
            }
        }
    }
}
