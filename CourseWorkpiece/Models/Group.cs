using System.ComponentModel.DataAnnotations;

namespace CourseWorkpiece.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public List<Students> Students { get; set; } //это структура для привязки

    }
}
