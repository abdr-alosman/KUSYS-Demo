using KUSYS_Demo.Data;
using KUSYS_Demo.Models.ViewModels;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KUSYS_Demo.Dto;
using KUSYS_Demo.Extentsions;
using Microsoft.AspNetCore.Authorization;

namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class StudentCoursesController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public StudentCoursesViewModel StudentCoursesVM { get; set; }
        public StudentCoursesController(ApplicationDbContext db)
        {
            _db = db;
            StudentCoursesVM = new StudentCoursesViewModel();
        }
        //US-4. List all Student & Course matchings in this page
        public IActionResult Index()
        {
            List<Student> stntCourses = new List<Student>();
            if (User.IsInRole("Admin"))
            {
                // A user with 'Admin' role can see all Student & Course matchings
                stntCourses = _db.Students.Include(x => x.StudentCourses).ThenInclude(x => x.Course).ToList();
            }
            else
            {
                // A user with 'User' role can see only her/his matchings
                string UserID = _db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefault().Id;
                stntCourses = _db.Students.Include(x => x.StudentCourses).ThenInclude(x => x.Course).Where(x => x.Id == UserID).ToList();

            }

            return View(stntCourses);
        }
        // Match a student with a selection of courses. 
        public IActionResult MatchCourse(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //A user with 'User' role can match only herself/himself with the selected courses, if he/she try to match other students using UserId or copy link will return NotFound page 
            if (User.IsInRole("User"))
            {
                string UserID = _db.Users.Where(x => x.UserName == User.Identity.Name).SingleOrDefault().Id;
                if (UserID != id) { return NotFound(); }
            }
            Student student = _db.Students.Find(id);
            if (student == null) { return NotFound(); }
            StudentCoursesVM.Student = student;
            StudentCoursesVM.CourseListVM = _db.Courses.ToList();
            StudentCoursesVM.StudentCourseListVM = _db.StudentCourses.ToList();
            return View(StudentCoursesVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MatchCourse(string studentId, List<string> CourseIds)
        {
            Student student = _db.Students.Include(x => x.StudentCourses).SingleOrDefault(x => x.Id == studentId);
            if (student == null) { return NotFound(); }

            // update user courses
            foreach (var item in student.StudentCourses)
            {
                _db.StudentCourses.Remove(item);
            }
            foreach (var item in CourseIds)
            {
                student.StudentCourses.Add(new StudentCourse(item));
            }

            _db.Students.Update(student);
            await _db.SaveChangesAsync();

            TempData.Put("message", new AlertMessage()
            {
                Message = "Student courses updated successfully",
                AlertType = "success"
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
