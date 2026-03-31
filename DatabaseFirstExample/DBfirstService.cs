using DatabaseFirstExample.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseFirstExample
{
    public class DBfirstService
    {
        public void Create(string name, int age)
        {
            using (var context = new AppDbContext())
            {
                var student = new Student
                {
                    Name = name,
                    Age = age
                };
                context.Students.Add(student);
                int result = context.SaveChanges();

                string message = result > 0 ? "Data inserted successfully" : "Data insertion failed.";
                Console.WriteLine(message);


            }
        }

        public void ReadAllData()
        {
            using (var context = new AppDbContext())
            {
                var students = context.Students.AsNoTracking().Where(x => !x.DeleteFlag).ToList();
                if (students.Any())
                {
                    foreach (var stu in students)
                    {
                        Console.WriteLine($"ID: {stu.Id}, No: {stu.Name}, Name: {stu.Age}");

                    }
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }

        public void Read(int id)
        {
            using (var context = new AppDbContext())
            {
                var student = context.Students.AsNoTracking().FirstOrDefault(x => x.Id == id);
                if (student != null)
                {
                    Console.WriteLine("ID: " + student.Id + "Name: " + student.Name + "Age: " + student.Age);
                }
                else
                {
                    Console.WriteLine("No data found.");
                }
            }
        }

        public void Update(int id, string name, int age)
        {
            using (var context = new AppDbContext())
            {

                var student = context.Students.FirstOrDefault(x => x.Id == id && !x.DeleteFlag);
                if (student != null)
                {
                    student.Name = name;
                    student.Age = age;
                    var result = context.SaveChanges();

                    string message = result > 0 ? "Data updated successfully" : "Data update failed";
                    Console.WriteLine(message);

                }
                else
                {
                    Console.WriteLine("Error");
                }

            }
        }

        public void Delete(int id)
        {
            using (var context = new AppDbContext())
            {
                var student = context.Students.FirstOrDefault(x => x.Id == id);
                if (student != null)
                {
                    student.DeleteFlag = true;
                    var result = context.SaveChanges();

                    string message = result > 0 ? "Data deleted successfully" : "Data deletetion failed";
                    Console.WriteLine(message);
                }
            }
        }
    }
}
