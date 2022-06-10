using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Identity;

namespace KUSYS_Demo.Data
{
    public static class SeedIndentity
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            var username = configuration["Data:AdminUser:username"];
            var email = configuration["Data:AdminUser:email"];
            var password = configuration["Data:AdminUser:password"];
            var role = configuration["Data:AdminUser:role"];
            if (await userManager.FindByEmailAsync(email) == null)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                var user = new User()
                {
                    UserName = username,
                    Email = email,
                    FirstName = "ABDULRAHMAN",
                    LastName = "ALOTHMAN",
                    EmailConfirmed = true,

                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
            if (roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }

            ApplicationDbContext db = new ApplicationDbContext();
            if (!db.Courses.Any())
            {
                var courses = new List<Course>
            {
                new Course { CourseId = "CSI101" ,CourseName="Introduction to Computer Science"},
                new Course { CourseId = "CSI102" ,CourseName="Algorithms"},
                new Course { CourseId = "MAT101" ,CourseName="Calculus"},
                new Course { CourseId = "PHY101",CourseName="Physics" }
            };
                db.AddRange(courses);
                db.SaveChanges();
            }

        }
    }
}
