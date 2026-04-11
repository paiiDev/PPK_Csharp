using DIExample.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DIExample.Models.Domain.DomainTodoDto;
using static DIExample.Models.DTOs.TodoDtoModel;

namespace DIExample.Interfaces
{
    public interface ITodoApi
    {
        [Get("/api/todos/{id}")]
        Task<TodoResponse<TodoDto>> GetTodo(int id);
    }
}
