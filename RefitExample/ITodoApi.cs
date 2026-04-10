using Refit;
using RefitExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RefitExample
{
    public interface ITodoApi
    {
        [Get("/api/todos/{id}")]
        Task<TodoResponse<TodoDto>> GetTodo(int id);
    }
}
