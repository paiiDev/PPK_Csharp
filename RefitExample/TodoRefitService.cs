using CsharpDotNet.shared;
using RefitExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefitExample
{
    public class TodoRefitService
    {
        private readonly ITodoApi _api;
        public TodoRefitService(ITodoApi api)
        {
            _api = api;
        }

        public async Task<Result<Todo>> GetTodo(int id)
        {
            try
            {
                var response = await _api.GetTodo(id);
                if (response is null)
                {
                    return Result<Todo>.Fail("API call returned null response");
                }
                if (!response.success)
                {
                    return Result<Todo>.Fail(response.message ?? "API returned failure");
                }
                var dto = response.data;
                if(dto is null)
                {
                    return Result<Todo>.Fail("No data returned");
                }
                var todo = new Todo
                {
                    id = dto.id,
                    title = dto.todo,
                    isDone = dto.completed,
                    userId = dto.userId,
                };
                return Result<Todo>.Success(todo);
            }
            catch (Exception ex)
            {
                return Result<Todo>.Fail(ex.Message);
            }
        }
    }
}
