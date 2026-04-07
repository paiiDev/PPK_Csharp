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
           app.MapGet("/todos/{id}", async (int id, ITodoService service) =>
            {
                try
                {
                    var data = await service.GetById(id);
                    if (!data.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(data.Error));
                    }
                    return Results.Ok(BaseResponse<TodoId>.Ok(data.Value));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName("Get todo by id")
            .WithOpenApi();
            app.MapPost("/todos", async (Todo newTodo, ITodoService service) =>
            {
                try
                {
                    var data = await service.CreateTodo(newTodo);
                    if (!data.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(data.Error));
                    }
                    return Results.Ok(BaseResponse<Todo>.Ok(data.Value));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName("Create todo")
            .WithOpenApi();
            app.MapPut("/todos/{id}", async (int id, Todo updateTodo, ITodoService service) =>
            {
                try
                {
                    var data = await service.UpdateTodo(id, updateTodo);
                    if (!data.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(data.Error));
                    }
                    return Results.Ok(BaseResponse<Todo>.Ok(data.Value));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName("Update todo")
            .WithOpenApi();
            app.MapDelete("/todos/{id}", async (int id, ITodoService service) =>
            {
                try
                {
                    var data = await service.DeleteTodo(id);
                    if (!data.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(data.Error));
                    }
                    return Results.Ok(BaseResponse<bool>.Ok(data.Value));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }
            }).WithName("Delete todo")
            .WithOpenApi();
        }
    }
}
