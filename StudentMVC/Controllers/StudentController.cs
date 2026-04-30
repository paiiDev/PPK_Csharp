using Microsoft.AspNetCore.Mvc;
using StudentMVC.Models;
using StudentMVC.Services;

namespace StudentMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
            public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _studentService.GetAllStudentsAsync();
            if (!result.IsSuccess)
            {
                ViewBag.Error = result.Error;
                return View(new List<StudentResponseDto>());
            }
            return View(result.Value);
        }
    }
}
