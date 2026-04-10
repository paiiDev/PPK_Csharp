using Refit;
using RefitExample;

var api = RestService.For<ITodoApi>("http://localhost:5095");

var service = new TodoRefitService(api);
Console.WriteLine("Fetching data from api....");
var result = await service.GetTodo(28);
if (!result.IsSuccess)
{
    Console.WriteLine(result.Error);
}


Console.WriteLine($"Title: {result.Value.title}");
Console.WriteLine($"IsDone: {result.Value.isDone}");

Console.ReadKey();
