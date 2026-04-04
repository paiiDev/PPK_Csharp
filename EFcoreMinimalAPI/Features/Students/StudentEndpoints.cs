
namespace EFcoreMinimalAPI.Features.Students
{
    public static class StudentEndpoints
    {
        public static void MapStudentEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/students", async (IStudentService service) =>
            {
                try
                {
                    var data = await service.ReadAllData();
                    return Results.Ok(data);
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

            })
                .WithName("Get all Students")
                .WithOpenApi();

            app.MapGet("/students/{id}", async (int id, IStudentService service) =>
            {
                try
                {
                    var student = await service.ReadById(id);
                    return student is null ? Results.NotFound(student) : Results.Ok(student);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            }).WithName("Get by id")
              .WithOpenApi();

            app.MapPost("/students", async (Student student, IStudentService service) =>
            {
                try
                {
                    var result = await service.Create(student);
                    return Results.Ok();
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);

                }

            }).WithName("Create student")
              .WithOpenApi();
            app.MapPatch("/students/{id}", async (int id, Student student, IStudentService service) =>
            {
                var existing = await service.ReadById(id);
                if (existing is null)
                {
                    return Results.NotFound();
                }
                await service.Update(id, student);
                return Results.Ok();
            }).WithName("Update student")
              .WithOpenApi();


            app.MapDelete("/students/{id}", async (int id, IStudentService service) =>
            {
                var existing = await service.ReadById(id);
                if (existing is null)
                {
                    return Results.NotFound();
                }
                await service.Delete(id);
                return Results.Ok();
            }).WithName("Delete student")
              .WithOpenApi();

        }
    }
}
