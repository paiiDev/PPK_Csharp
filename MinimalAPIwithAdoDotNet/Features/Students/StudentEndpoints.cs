namespace MinimalAPIwithAdoDotNet.Features.Students
{
    public static class StudentEndpoints
    {
            public static void MapStudentEndpoints(this IEndpointRouteBuilder app)
            {
                app.MapGet("/students", async ( StudentService service) =>
                {
                    try
                    {
                        var data = await service.ReadAll();
                        return Results.Ok(data);
                    }
                    catch (Exception ex)
                    {
                        return Results.Problem(ex.Message);
                    }

                });

            app.MapGet("/students/{id}", async (int id,StudentService service) =>
            {
                try
                {
                    var student = await service.Read(id);
                    return student is null ? Results.NotFound(student) : Results.Ok(student);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            app.MapPost("/students", async (Student student, StudentService service) =>
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
                
            });
            app.MapPatch("/students/{id}", async (int id, Student student, StudentService service) =>
            {
                var existing = await service.Read(id);
                if(existing is null)
                {
                    return Results.NotFound();
                }
                await service.Update(id, student);
                return Results.Ok();
            });
            app.MapDelete("/students/{id}", async (int id, StudentService service) =>
            {
                var existing = await service.Read(id);
                if(existing is null)
                {
                    return Results.NotFound();
                }
                await service.Delete(id);
                return Results.Ok();    
            });
                
            }
        

    }
}
