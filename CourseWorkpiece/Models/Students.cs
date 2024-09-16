using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkpiece.Models
{
    public class Students
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
        public int GroupId { get; set; }
        [Required]
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
