using CsharpDotNet.shared;
using RestClientExample.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RestClientExample.Servicies
{
    public class TodoRestClientService
    {
        private readonly RestClient _client;
        public TodoRestClientService()
        {
            _client = new RestClient("http://localhost:5095");
        }

        public async Task<Result<Todo>> GetTodo(int id)
        {
            var request = new RestRequest($"/api/todos/{id}", Method.Get);
            var response = await _client.ExecuteAsync<TodoResponse<TodoDto>>(request);

            if (!response.IsSuccessful || response.Data is null)
            {
                return Result<Todo>.Fail("API call failed");
            }

            var dto = response.Data.data;
            if (dto is null)
            {
                return Result<Todo>.Fail("No data return");
            }

            var todo = new Todo
            {
                id = dto.id,
                todo = dto.todo,
                completed = dto.completed,
                userId = dto.userId,
            };


            return Result<Todo>.Success(todo);

        }
    }
}
