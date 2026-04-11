using DIExample.Interfaces;
using DIExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DIExample.Models.Domain.DomainTodoDto;

namespace DIExample.Services
{
    public class TodoRefitService : ITodoRefitService
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
                if (response is null || response.data is null)
                {
                    return Result<Todo>.Fail("Response content is null..");
                }
                var todo = new Todo
                {
                    id = response.data.id,
                    title = response.data.todo,
                    isDone = response.data.completed,
                    userId = response.data.userId,
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
