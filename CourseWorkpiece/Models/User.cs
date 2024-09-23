using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("sGroup")]
        public int sGroupId { get; set; }
        public sGroup sGroup { get; set; }

    }
}
