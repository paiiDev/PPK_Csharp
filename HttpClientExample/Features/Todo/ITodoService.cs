using CsharpDotNet.shared;

namespace HttpClientExample.Features.Todo
{
    public interface ITodoService
    {
        public Task<Result<List<Todo>>> GetAllData();
    }
}
