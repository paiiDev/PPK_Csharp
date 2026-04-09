using CsharpDotNet.shared;
using System.Data;
using System.Text.Json;

namespace HttpClientExample.Features.Todo
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "http://localhost:5095/api/todos";
        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<List<Todo>>> GetAllData()
        {
            try
            {
                var response = await _httpClient.GetAsync(_baseUrl);

                if (!response.IsSuccessStatusCode)
                {
                    return Result<List<Todo>>.Fail($"Failed to fetch data: {response.ReasonPhrase}");
                }

                var data = await response.Content.ReadFromJsonAsync<TodoResponse<List<TodoDto>>>();
                if (data is null || data.data is null)
                {
                    return Result<List<Todo>>.Fail("Response content is null");
                }
                var todos = data.data.Select(dto => new Todo
                {
                    id = dto.id,
                    todoTitle = dto.todo,
                    isDone = dto.completed,
                    userId = dto.userId
                }).ToList();
                return Result<List<Todo>>.Success(todos);
            }
            catch (Exception ex)
            {
                return Result<List<Todo>>.Fail(ex.Message);
            }
        }
        public async Task<Result<Todo>> GetById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return Result<Todo>.Fail($"Failed to fetch data: {response.ReasonPhrase}");
                }
                var data = await response.Content.ReadFromJsonAsync<TodoResponse<TodoDto>>();
               
                if (data is null || data.data is null)
                {
                    return Result<Todo>.Fail("Response content is null or empty");
                }
                var todo = new Todo
                {
                    id = data.data.id,
                    todoTitle = data.data.todo,
                    isDone = data.data.completed,
                    userId = data.data.userId,
                };

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
                var response = await _httpClient.PostAsJsonAsync(_baseUrl, newTodo);
                if (!response.IsSuccessStatusCode)
                {
                    return Result<Todo>.Fail($"Failed to create todo: {response.ReasonPhrase}");
                }
                var data = await response.Content.ReadFromJsonAsync<TodoResponse<TodoDto>>();
                if (data is null || data.data is null || data.data.todo is null)
                {
                    return Result<Todo>.Fail("Response content is null");
                }
                var todo = new Todo
                {
                    id = data.data.id,
                    todoTitle = data.data.todo,
                    isDone = data.data.completed,
                    userId = data.data.userId
                };
                return Result<Todo>.Success(todo);
                {

                }
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
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{id}", updatedTodo);
                if (!response.IsSuccessStatusCode)
                {
                    return Result<Todo>.Fail($"Failed to update todo: {response.ReasonPhrase}");
                }

                return Result<Todo>.Success(updatedTodo);
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
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return Result<bool>.Fail($"Failed to delete todo: {response.ReasonPhrase}");
                }
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Fail(ex.Message);
            }
        }
    }
}
