using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Student : User
    {
        [Required(ErrorMessage = "Please enter date of birth")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public bool Status { get; set; }
        public long StudentNo { get; set; }
        public List<StudentCourse>? StudentCourses { get; set; }
        public Student()
        {
            Status = true;
            StudentCourses=new List<StudentCourse>();
        }
    }
}
