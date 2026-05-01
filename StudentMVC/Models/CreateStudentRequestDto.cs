using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class CreateStudentRequestDto
    {

        [Required]
        public string Name { get; set; }

        [Range(5,100)]
        public int Age { get; set; }
    }
}
