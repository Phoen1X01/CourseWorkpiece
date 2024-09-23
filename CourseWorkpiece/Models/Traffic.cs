using CourseWorkpiece.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseWorkpiece.Models
{
    public class Traffic
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TrafficNumber { get; set; }

        [Required]
        public TypeTraffic TypeTraffic { get; set; }

        [Required]
        public int StudentId { get; set; }
        [Required]
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        [Required]
        public int LectureId { get; set; }
        [Required]
        [ForeignKey("LectureId")]
        public Lecture Lecture { get; set; }
    }
}
