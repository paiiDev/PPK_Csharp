using JWT_EncDec_Example.DTOs;
using JWT_EncDec_Example.Filters;
using JWT_EncDec_Example.Services.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JWT_EncDec_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public IActionResult CreateStudent([FromBody] StudentRequestDto request)
        {
            if (request is null)
            {
                return BadRequest(new { Message = "Student request is required." });
            } 

            var result = _studentService.CreateStudent(request);
            return Ok(result);
        }


        [ServiceFilter(typeof(PerformanceLogFilter))]
        [HttpGet("Get all student")]
        public IActionResult GetAllStudent()
        {
            var result = _studentService.GetStudents();
            return Ok(result);
        }
    }
}
