using System.ComponentModel.DataAnnotations;

namespace StudentMVC.Models
{
    public class EditStudentRequestDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Range(5, 100)]
        public int Age { get; set; }
    }
}
