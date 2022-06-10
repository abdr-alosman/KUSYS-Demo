using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KUSYS_Demo.Models
{
    public class StudentCourse
    {
        [Key]
        public int Id { get; set; }
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
        public string CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course? Course  { get; set; }
        public StudentCourse()
        {

        }
        public StudentCourse(string courseid)
        {
            CourseId = courseid;
        }
    }
}
