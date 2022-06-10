using KUSYS_Demo.Dto;
using KUSYS_Demo.Extentsions;
using KUSYS_Demo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            // Register a new system user ( a non-student user ) with default role ( User)
            // Admin can change user role later if he want

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
           
            User user = new User
            {
                FirstName = char.ToUpper(model.FirstName[0]) + model.FirstName.Substring(1).ToLower(),
                LastName = char.ToUpper(model.LastName[0]) + model.LastName.Substring(1).ToLower(),
                Email = model.Email,
                UserName = model.Email,
                EmailConfirmed = true,

            };
            // Add user to Users table with (User) role
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            string role = "User";
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
                TempData.Put("message", new AlertMessage()
                {
                    Message = "Your account has been successfully created",
                    AlertType = "success"
                });
                
                return RedirectToAction("UserList", "Admin");

            }
            else
            {
                result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
            }
            return View(model);
        }
        public IActionResult Login(string? ReturnUrl = null)
        {
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", "No account has been created before with this User Email!");
                return View(model);
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                if (result.Succeeded)
                {
                    return Redirect(model.ReturnUrl ?? "~/");
                }
                ModelState.AddModelError("", "Incorrect username or password!");
            }
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Logout(RegisterModel model)
        {
            await _signInManager.SignOutAsync();
            return Redirect(nameof(Login));
        }
    }
}
