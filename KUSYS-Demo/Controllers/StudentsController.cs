using KUSYS_Demo.Data;
using KUSYS_Demo.Dto;
using KUSYS_Demo.Extentsions;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace KUSYS_Demo.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private UserManager<User> _userManager;
       
        public StudentsController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
            
        }
        // List all Students in this page
        public IActionResult Index()
        {
            List<Student> students = _db.Students.Where(x => x.Status == true).ToList();
            return View(students);
        }

        // Create a new student (only Admin)
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new StudentModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentModel model)
        {
            // Create a new student, default password will be a student no
            if (ModelState.IsValid)
            {
                Student student = new Student();
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.BirthDate = model.BirthDate;
                student.Email = model.Email;
                student.EmailConfirmed = true;
                student.UserName = model.Email;
               
                student.StudentNo= model.StudentNo;

                string role = "User";

                IdentityResult result = await _userManager.CreateAsync(student, model.StudentNo.ToString());
                
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(student, role);
                    TempData.Put("message", new AlertMessage()
                    {
                        Message = "Student registered successfully",
                        AlertType = "success"
                    });
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                }
            }
            return View(model);
        }
        // edit student
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Student student = _db.Students.Find(id);
            if (student == null) { return NotFound(); }
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student model)
        {
            if (ModelState.IsValid)
            {
                 
                Student student = _db.Students.Find(model.Id);
                if (student == null) { return NotFound();}

                student.FirstName = model.FirstName;
                student.LastName= model.LastName;
                student.BirthDate= model.BirthDate;
                student.Email= model.Email;
                student.EmailConfirmed = true;
                student.UserName = model.Email;

                
                student.StudentNo = model.StudentNo;

                _db.Students.Update(student);
                await _db.SaveChangesAsync();

                TempData.Put("message", new AlertMessage()
                {
                    Message = "Student updated successfully",
                    AlertType = "success"
                });
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            Student stdnt = _db.Students.Find(id);
            _db.Students.Remove(stdnt);
            _db.SaveChanges();

            TempData.Put("message", new AlertMessage()
            {
                Message = "Student deleted successfully",
                AlertType = "success"
            });
            return RedirectToAction(nameof(Index));
        }
    }
}
