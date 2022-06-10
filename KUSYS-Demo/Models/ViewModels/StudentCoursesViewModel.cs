namespace KUSYS_Demo.Models.ViewModels
{
    public class StudentCoursesViewModel
    {
        public Student Student  { get; set; }
        public StudentCourse StudentCourse { get; set; }
       
        public IEnumerable<Student> StudentListVM { get; set; }
        public IEnumerable<Course> CourseListVM { get; set; }
        public IEnumerable<StudentCourse> StudentCourseListVM { get; set; }
    }
}
