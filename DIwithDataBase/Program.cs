using DIwithDataBase.DataAccess;
using DIwithDataBase.DTOs;
using DIwithDataBase.Interfaces;
using DIwithDataBase.Models;
using DIwithDataBase.Repositories;
using DIwithDataBase.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddUserSecrets<Program>();
    })
    .ConfigureServices((context, services) =>
    {
        string dbString = context.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(dbString);
        });

        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IStudentService, StudentService>();

    });



var host = builder.Build();



var service = host.Services.GetRequiredService<IStudentService>();

////var result = await service.GetStudent(12);
//var result = await service.GetStudents();
//if (!result.IsSuccess)
//{
//    Console.WriteLine(result.Error);
//}

//foreach(var student in result.Value)
//{
//    Console.WriteLine($"Name: {student.Name}");
//    Console.WriteLine($"Age: {student.Age}");
//}

//Console.WriteLine("Please enter student name");
//var name = Console.ReadLine();
//Console.WriteLine("Please enter student age");
//var age = Convert.ToInt32(Console.ReadLine());
//var student = new DomainStudentDto
//{
//    Name = name,
//    Age = age
//};

//var result = await service.CreateStudentAsync(student);

//var result = await service.UpdateStudent(new DomainStudentDto
//{
//    Id = 12,
//    Name = "Chole",
//    Age = 30
//});

Console.WriteLine("Enter student id to delete:");
var id = Convert.ToInt32(Console.ReadLine());

var result = await service.DeleteStudent(id);

if (!result.IsSuccess)
{
    Console.WriteLine(result.Error);
}
else
{
    Console.WriteLine("Student deleted successfully");
}

