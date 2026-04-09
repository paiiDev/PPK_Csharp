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

            app.MapGet("/api/todos/{id}", async (int id, ITodoApiService service) =>
            {
                var result = await service.GetTodoById(id);
                if (!result.IsSuccess)
                {
                    return Results.NotFound(BaseResponse<string>.Fail(result.Error));
                }
                return Results.Ok(BaseResponse<Todo>.Ok(result.Value, "Todo retrieved successfully."));
            }).WithName("Get todo by id")
            .WithOpenApi();

            app.MapPost("/api/todos", async (Todo newTodo, ITodoApiService service) =>
            {
                var result = await service.CreateTodo(newTodo);
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(BaseResponse<string>.Fail(result.Error));
                }
                return Results.Ok(BaseResponse<Todo>.Ok(newTodo));
            }).WithName("Create toto")
            .WithOpenApi();

            app.MapPut("/api/todos/{id}", async (int id, Todo updatedTodo, ITodoApiService service) =>
            {
                var result = await service.UpdateTodo(id, updatedTodo);
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(BaseResponse<string>.Fail(result.Error));
                }
                return Results.Ok(BaseResponse<bool>.Ok(true));
            }).WithName("Update todo")
            .WithOpenApi();

            app.MapDelete("/api/todos/{id}", async (int id, ITodoApiService serive) =>
            {
                var result = await serive.DeleteTodo(id);
                if (!result.IsSuccess)
                {
                    return Results.BadRequest(BaseResponse<string>.Fail(result.Error));
                }
                return Results.Ok(BaseResponse<bool>.Ok(true));
            }).WithName("Delete todo by id")
            .WithOpenApi();
        }


    }
}
