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


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _studentService.CreateStudentAsync(request);
            if (!result.IsSuccess)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = result.Error;
                return View(request);
            }
            TempData["IsSuccess"] = true;
            TempData["Message"] = "Student Created Successfully";
            return RedirectToAction("Index");
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
        public async Task<IActionResult> Edit(EditStudentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            var result = await _studentService.UpdateStudentAsync(request.Id, request);
            if (!result.IsSuccess)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = result.Error;
                return View(request);
            }
            TempData["IsSuccess"] = true;
            TempData["Message"] = "Student Updated Successfully";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student is null)
            {
                return RedirectToAction("Index");
            }
            return View(student.Value);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result.IsSuccess)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = result.Error;
                return RedirectToAction("Index");
            }
            TempData["IsSuccess"] = true;
            TempData["Message"] = "Student Deleted Successfully";
            return RedirectToAction("Index");

        }
    }
}   