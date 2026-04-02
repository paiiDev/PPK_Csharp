using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Services;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentService _studentService;
        public StudentsController(StudentService service){
            _studentService = service;
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _studentService.GetStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            var student = _studentService.GetStudent(id);
            if (student == null)
            {
                return NotFound("No data found");
            }
            return Ok(student);
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
           _studentService.Create(student);
            return Ok("Data created successfully.");

        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, Student student)
        {
            var existingStudent = _studentService.GetStudent(id);
            if (existingStudent == null)
            {
                return NotFound("No data found");
            }
            _studentService.Update(id, student);
            return Ok("Data updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingStudent = _studentService.GetStudent(id);
            if (existingStudent == null)
            {
                return NotFound("No data found");
            }
            _studentService.Delete(id);
            return Ok("Data deleted successfully.");
        }
        }
}
