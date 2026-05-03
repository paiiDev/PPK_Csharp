using Microsoft.AspNetCore.Mvc;
using StudentMVCajax.Models;
using StudentMVCajax.Services;

namespace StudentMVCajax.Controllers
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
            return View();
        }

        public async Task<IActionResult> GetAllStudents()
        {
            var result = await _studentService.GetAllStudentsAsync();
            Console.WriteLine(result);
            if (!result.IsSuccess)
            {
                return Json(new { isSuccess = false, message = result.Error });
            }
            return Json(new { isSuccess = true, data = result.Value });
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody]CreateStudentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new {isSuccess = false, message = "Invalid data"});
            }

            var result = await _studentService.CreateStudentAsync(request);

            if (!result.IsSuccess)
            {
                return Json(new { isSuccess = false, message = result.Error });
            }

            return Json(new { isSuccess = true, message = "Student created successfully" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (!student.IsSuccess)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = student.Error;
                return RedirectToAction("Index");
            }
            var requestModel = new EditStudentRequestDto
            {
                Id = student.Value.Id,
                Name = student.Value.Name,
                Age = student.Value.Age
            };
            return View(requestModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAjax([FromBody]EditStudentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { isSuccess = false, message = "Invalid data" });
            }
            var result = await _studentService.UpdateStudentAsync(request.Id, request);
            if (!result.IsSuccess)
            {
               return Json(new { isSuccess = false, message = result.Error });
            }
            return Json(new { isSuccess = true, message = "Student updated successfully" });
        }

        

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(id <= 0)
            {
                return Json(new { isSuccess = false, message = "Invalid student ID" });
            }
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result.IsSuccess)
            {
                return Json(new { isSuccess = false, message = result.Error });
            }
            return Json(new { isSuccess = true, message = "Student Deleted Successfully" }) ;

        }
    }
}   