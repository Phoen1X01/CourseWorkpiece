using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkpiece.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string MiddleName { get; set; }

        [Required]
        public int sGroupId { get; set; }
        [Required]
        [ForeignKey("sGroupId")]
        public sGroup sGroup { get; set; }

        [Required]
        public List<Traffic> Traffics { get; set; }
    }
}
