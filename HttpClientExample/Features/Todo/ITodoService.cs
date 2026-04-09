using CsharpDotNet.shared;

namespace HttpClientExample.Features.Todo
{
    public interface ITodoService
    {
        public Task<Result<List<Todo>>> GetAllData();
        public Task<Result<Todo>> GetById(int id);
        public Task<Result<Todo>> CreateTodo(Todo newTodo);
        public Task<Result<Todo>> UpdateTodo(int id, Todo updatedTodo);
        public Task<Result<bool>> DeleteTodo(int id);   
    }
}
