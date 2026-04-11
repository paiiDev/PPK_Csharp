using DIExample.Config;
using DIExample.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddServices();
    });

Console.WriteLine("Fetching data...");

var host = builder.Build();
var service = host.Services.GetRequiredService<ITodoRefitService>();

var result = await service.GetTodo(28);


if(!result.IsSuccess || result is null || result.Value is null)
{
    Console.WriteLine(result.Error);
    Environment.ExitCode = 2;
    return;
}

    var todo = result.Value;
    Console.WriteLine($"Title: {todo.title}");
    Console.WriteLine($"IsDone: {todo.isDone}");


Console.ReadKey();