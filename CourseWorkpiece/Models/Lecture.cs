using System.ComponentModel.DataAnnotations;
using System.Data;

namespace CourseWorkpiece.Models
{
    public class Lecture
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
