using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper
{
    public class DapperService
    {
        private readonly string _connectionString;
        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Create(string name, int age)
        {
            using (IDbConnection db = new SqlConnection(_connectionString) )
            {
                string Query = $@"INSERT INTO [dbo].[Students]
                               ([Name]
                               ,[Age])
                                VALUES
                               (@name,
                               @age)";

               var result = db.Execute(Query, new { name, age });
                if(result > 0)
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string Query = $@"SELECT [Id]
                ,[Name]
                ,[Age]
                 FROM [dbo].[Students]";
                var students = db.Query<student>(Query).ToList();
                if (students.Any())
                {
                    foreach (var student in students)
                    {
                        Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                    }
                } else
                {
                    Console.WriteLine("No data flound.");
                }
               
            }
        }

        public void Read(int id)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                string Query = $@"SELECT [Id]
                ,[Name]
                ,[Age]
                 FROM [dbo].[Students] WHERE Id = @id";
                var student = db.QueryFirstOrDefault<student>(Query, new { id });
                if(student != null)
                {
                    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");
                } else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }

        public void Update(int id, string name, int age)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string Query = $@"UPDATE [dbo].[Students]
                                SET [Name] = @name,
                                    [Age] = @age
                                WHERE Id = @id";
                var result = db.Execute(Query, new { id, name, age });
                if(result > 0)
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
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string Query = $@"DELETE FROM [dbo].[Students] WHERE Id = @id";
                var result = db.Execute(Query, new { id });
                if(result > 0)
                {
                    Console.WriteLine("Data deleted successfully.");
                } else
                {
                    Console.WriteLine("Data deletion failed.");
                }
            }
        }

        public class student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
