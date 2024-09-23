using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkpiece.Models
{
    public class sGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NameGroup { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public List<Student> Students { get; set; }
    }
}
