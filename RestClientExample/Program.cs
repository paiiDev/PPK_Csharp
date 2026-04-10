using RestClientExample.Servicies;

Console.WriteLine("Fetching the data....");

var service = new TodoRestClientService();

//var result = await service.GetTodo(28);

//if (!result.IsSuccess)
//{
//    Console.WriteLine($"Error: {result.Error}");
//}

//Console.WriteLine($"Todo: {result.Value.todo}");
//Console.WriteLine($"Completed: {result.Value.completed} ");
//Console.WriteLine($"UserID: {result.Value.userId}");

var result = await service.GetAllTodo();
var todos = result.Value;
if (!result.IsSuccess)
{
    Console.WriteLine($"Error: {result.Error}");
}
foreach(var todo in todos)
{
    Console.WriteLine($"Todo: {todo.todo}");
    Console.WriteLine($"Completed: {todo.completed} ");
    Console.WriteLine($"UserID: {todo.userId}");
    Console.WriteLine("________________________");
}


Console.ReadLine();