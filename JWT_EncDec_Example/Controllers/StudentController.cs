using JWT_EncDec_Example.DTOs;
using JWT_EncDec_Example.Services.Student;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT_EncDec_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [Authorize("AdminOnly")]
        [HttpPost("Admin")]
        public IActionResult CreateStudent(StudentRequestDto request)
        {
            var result = _studentService.CreateStudent(request);
            if(request.Id == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}
