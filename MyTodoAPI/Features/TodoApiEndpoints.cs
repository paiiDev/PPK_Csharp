using CsharpDotNet.shared;
using System.Runtime.CompilerServices;

namespace MyTodoAPI.Features
{
    public static class TodoApiEndpoints
    {
        public static void MapTodoApiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/api/todos", async (ITodoApiService service) =>
            {
                var result = await service.GetAllTodos();
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(BaseResponse<String>.Fail(result.Error));
                }
                return Results.Ok(BaseResponse<List<Todo>>.Ok(result.Value, "Todos retrieved successfully."));
            }).WithName("Get Tod0s")
            .WithOpenApi();
        }
    }
}
