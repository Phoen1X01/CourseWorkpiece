using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        public string MiddleName { get; set; }

        [Required]
        public int sGroupId { get; set; }
        [ForeignKey("sGroupId")]
        [JsonIgnore]
        public sGroup sGroup { get; set; }


        [JsonIgnore]
        public List<Traffic> Traffics { get; set; }
    }
}
