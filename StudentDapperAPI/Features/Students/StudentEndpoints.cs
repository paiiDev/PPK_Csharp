using StudentDapperAPI.Common;

namespace StudentDapperAPI.Features.Students
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
                    if (!data.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(data.Error)); ;
                    }
                    return Results.Ok(BaseResponse<List<Student>>.Ok(data.Value));
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

            });
            app.MapGet("/students/{id}", async (int id, IStudentService service) =>
            {
                try
                {
                    var student = await service.ReadById(id);
                    if (!student.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(student.Error));
                    }
                    return Results.Ok(BaseResponse<Student>.Ok(student.Value));
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);
                }
            });

            app.MapPost("/students", async (Student student, IStudentService service) =>
            {
                try
                {
                    var result = await service.Create(student);
                    if (!result.IsSuccess)
                    {
                        return Results.BadRequest(BaseResponse<string>.Fail(result.Error));
                    }
                    return Results.Ok(BaseResponse<bool>.Ok(result.Value));
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex);

                }

            });
            app.MapPatch("/students/{id}", async (int id, Student student, IStudentService service) =>
            {
                var existing = await service.ReadById(id);
                if (!existing.IsSuccess)
                {
                    return Results.BadRequest(BaseResponse<bool>.Fail(existing.Error));
                }
                var result = await service.Update(id, student);
                return Results.Ok(BaseResponse<bool>.Ok(result.Value));
            });

            app.MapDelete("/students/{id}", async (int id, IStudentService service) =>
            {
                var existing = await service.ReadById(id);
                if (!existing.IsSuccess)
                {
                    return Results.BadRequest(BaseResponse<bool>.Fail(existing.Error));
                }
                var result = await service.Delete(id);
                return Results.Ok(BaseResponse<Boolean>.Ok(result.Value));
            });

        }
    }
}
