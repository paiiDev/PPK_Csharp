using CsharpDotNet.shared;

namespace MyTodoAPI.Features
{
    public interface ITodoApiService
    {
        public Task<Result<List<Todo>>> GetAllTodos();
        public Task<Result<Todo>> GetTodoById(int id);
        public Task<Result<Todo>> CreateTodo(Todo newTodo);
        public Task<Result<Todo>> UpdateTodo(int id, Todo updatedTodo);
        public Task<Result<bool>> DeleteTodo(int id);
    }
}
