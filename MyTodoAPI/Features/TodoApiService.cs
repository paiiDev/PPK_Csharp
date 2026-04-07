using CsharpDotNet.shared;
using System.Net.Http;
using System.Text.Json;

namespace MyTodoAPI.Features
{
    public class TodoApiService : ITodoApiService
    {
        private readonly string _filePath = "C:\\Users\\PAII\\Desktop\\PPK_Csharp\\MyTodoAPI\\data\\todos.json";

        private async Task<List<Todo>> ReadAllData()
        {
            if(!File.Exists(_filePath))
            {
                return new List<Todo>();
            }
            var jsonString = await File.ReadAllTextAsync(_filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var response = JsonSerializer.Deserialize<TodoAPIResponse>(jsonString, options);
            return response?.todos.ToList() ?? new List<Todo>();
        }
        private async Task WriteAllData(List<Todo> todos)
        {
            var response = new TodoAPIResponse { todos = todos.ToArray() };
            var jsonString = JsonSerializer.Serialize(response, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_filePath, jsonString);
        }
        public async Task<Result<List<Todo>>> GetAllTodos()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return Result<List<Todo>>.Fail("Data file not found");
                }
               var todos = await ReadAllData();
                if (todos is null)
                {
                    return Result<List<Todo>>.Fail("Failed to parse data");
                }
                return Result<List<Todo>>.Success(todos);
            }
            catch (Exception ex)
            {
                return Result<List<Todo>>.Fail(ex.Message);
            }
        }
        public async Task<Result<Todo>> GetTodoById(int id)
        {
            try
            {
                var todos = await ReadAllData();
                var todo = todos.FirstOrDefault(t => t.id == id);
                if (todo is null)
                {
                    return Result<Todo>.Fail("Todo not found");
                }
                return Result<Todo>.Success(todo);
            }
            catch (Exception ex)
            {
                return Result<Todo>.Fail(ex.Message);
            }
        }
        public async Task<Result<Todo>> CreateTodo(Todo newTodo)
        {
            try
            {
                var todos = await ReadAllData();
                newTodo.id = todos.Any() ? todos.Max(t => t.id) + 1 : 1;
                todos.Add(newTodo);
                await WriteAllData(todos);
                return Result<Todo>.Success(newTodo);

            }
            catch (Exception ex)
            {
                return Result<Todo>.Fail(ex.Message);
            }

        }
        public async Task<Result<Todo>> UpdateTodo(int id, Todo updatedTodo)
        {
            try
            {
                var todos = await ReadAllData();
                var existingTodo = todos.FirstOrDefault(t => t.id == id);
                if(existingTodo is null)
                {
                    return Result<Todo>.Fail("Todo not found");
                }
                existingTodo.todo = updatedTodo.todo;
                existingTodo.completed = updatedTodo.completed;
                existingTodo.userId = updatedTodo.userId;
                await WriteAllData(todos);
                return Result<Todo>.Success(existingTodo);
            }
            catch (Exception ex)
            {
                return Result<Todo>.Fail(ex.Message);
            }
        }
        public async Task<Result<bool>> DeleteTodo(int id)
        {
            try
            {
                var todos = await ReadAllData();
                var todoToRemove = todos.FirstOrDefault(t => t.id == id);
                if (todoToRemove is null)
                {
                    return Result<bool>.Fail("Todo not found");
                }
                todos.Remove(todoToRemove);
                await WriteAllData(todos);
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ex.Message);
            }
        }
        }
}
