using CsharpDotNet.shared;

namespace MyTodoAPI.Features
{
    public interface ITodoApiService
    {
        public Task<Result<List<Todo>>> GetAllTodos();
    }
}
