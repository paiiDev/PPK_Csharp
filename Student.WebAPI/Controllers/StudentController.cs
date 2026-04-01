using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Student.Database.DataAccess;

namespace Student.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = new AppDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetStudetns()
        {
            var lst = await _context.Students.AsNoTracking().Where(x => !x.DeleteFlag).ToListAsync();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            var result = await _context.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if(result is null)
            {
                return NotFound("NO data found");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(TblStudent stu)
        {
            await _context.Students.AddAsync(stu);
            var result = _context.SaveChanges();
            return Ok(result > 0 ? "Created student successfully" : "Student creation failed.");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStudent(int id,TblStudent stu)
        {
            var item = await _context.Students.FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if (item is null)
            {
                return NotFound("No data found");
            }

            item.Name = stu.Name;
            item.Age = stu.Age;
            var result = await _context.SaveChangesAsync();
            return Ok(result > 0 ? "Data updated successfully." : "Data updation fialed.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var item = await _context.Students.FirstOrDefaultAsync(x => x.Id == id && !x.DeleteFlag);
            if(item is null) {
                return NotFound("No data found");
            }
            item.DeleteFlag = true;
            var result = await _context.SaveChangesAsync();
            return Ok(result > 0 ? "Data deleted successfully." : "Data deletion fialed.");
        }
    }
}
