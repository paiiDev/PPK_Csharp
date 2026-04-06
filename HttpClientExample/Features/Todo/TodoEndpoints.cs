using CsharpDotNet.shared;
using System.Runtime.CompilerServices;

namespace HttpClientExample.Features.Todo
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/todos", async (ITodoService service) =>
            {
                try
                {
                    var data = await service.GetAllData();
                    if (!data.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(data.Error));
                    }
                    return Results.Ok(BaseResponse<List<Todo>>.Ok(data.Value));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName("All todos")
            .WithOpenApi();
        }
    }
}
