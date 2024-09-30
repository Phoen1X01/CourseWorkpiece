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
        public DateOnly Date { get; set; }

        public List<Traffic> Traffics { get; set; }
    }
}
