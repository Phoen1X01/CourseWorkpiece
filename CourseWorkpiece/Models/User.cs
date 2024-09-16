using System.ComponentModel.DataAnnotations;

namespace CourseWorkpiece.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string PasswordHes { get; set; }

    }
}
