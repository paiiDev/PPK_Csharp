using DIExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DIExample.Models.Domain.DomainTodoDto;

namespace DIExample.Interfaces
{
    public interface ITodoRefitService
    {
         Task<Result<Todo>> GetTodo(int id);
    }
}
