using CsharpDotNet.shared;
using System.Text.Json;

namespace MyTodoAPI.Features
{
    public class TodoApiService : ITodoApiService
    {
        private readonly string _filePath = "C:\\Users\\PAII\\Desktop\\PPK_Csharp\\MyTodoAPI\\data\\todos.json";

        public async Task<Result<List<Todo>>> GetAllTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return Result<List<Todo>>.Fail("Data file not found");
                }
                string jsonString = await File.ReadAllTextAsync(_filePath);
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var todos = JsonSerializer.Deserialize<TodoAPIResponse>(jsonString, options);
                if (todos is null)
                {
                    return Result<List<Todo>>.Fail("Failed to parse data");
                }
                return Result<List<Todo>>.Success(todos.todos.ToList());
            }
            catch (Exception ex)
            {
                return Result<List<Todo>>.Fail(ex.Message);
            }
        }
    }
}
