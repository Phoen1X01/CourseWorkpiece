using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CourseWorkpiece.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        [JsonIgnore]
        public string PasswordHes { get; set; }

        [ForeignKey("sGroup")]
        [JsonIgnore]
        public int sGroupId { get; set; }
        public sGroup sGroup { get; set; }

        [Required]
        [JsonIgnore]
        public List<Session> Sessions { get; set; }

    }
}
