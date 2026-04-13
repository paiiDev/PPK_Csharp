using DIwithDataBase.DataAccess;
using DIwithDataBase.Interfaces;
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

//var result = await service.GetStudent(12);
var result = await service.GetStudents();
if (!result.IsSuccess)
{
    Console.WriteLine(result.Error);
}

foreach(var student in result.Value)
{
    Console.WriteLine($"Name: {student.Name}");
    Console.WriteLine($"Age: {student.Age}");
}