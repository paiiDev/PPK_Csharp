using System.ComponentModel.DataAnnotations;

namespace StudentMVCajax.Models
{
    public class CreateStudentRequestDto
    {

        [Required]
        public string Name { get; set; }

        [Range(5,100)]
        public int Age { get; set; }
    }
}
