using CsharpDotNet.shared;
using System.Text.Json;

namespace HttpClientExample.Features.Todo
{
    public class TodoService : ITodoService
    {
        private readonly HttpClient _httpClient;
        public TodoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<Result<List<Todo>>> GetAllData()
        {
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5095/api/todos");

                if (!response.IsSuccessStatusCode){
                    return Result<List<Todo>>.Fail($"Failed to fetch data: {response.ReasonPhrase}");
                }

                var data = await response.Content.ReadFromJsonAsync<TodoResponse>();
                if(data is null || data.data is null)
                {
                    return Result<List<Todo>>.Fail("Response content is null");
                }
                return Result<List<Todo>>.Success(data.data.ToList());
            } catch(Exception ex)
            {
                return Result<List<Todo>>.Fail(ex.Message);
            }
        }
    }
}
