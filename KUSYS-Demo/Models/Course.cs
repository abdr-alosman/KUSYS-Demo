using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.Models
{
    public class Course
    {
        [Key]
        public string CourseId { get; set; }
        public string? CourseName { get; set; }
        public List<StudentCourse>? StudentCourses { get; set; }
    }
}
