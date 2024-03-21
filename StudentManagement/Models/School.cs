using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Models
{
    public class School
    {
        public int SchoolID { get; set; }
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string SchoolAdress { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
