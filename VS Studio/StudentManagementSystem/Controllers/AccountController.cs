using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;
using StudentManagementSystem.ViewModels;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = HashPassword(model.Password);
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == hashedPassword);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.UserId.ToString());
                    HttpContext.Session.SetString("UserName", user.FullName);
                    HttpContext.Session.SetString("UserRole", user.Role);

                    if (user.Role == "Teacher")
                    {
                        return RedirectToAction("Index", "TeacherDashboard");
                    }
                    else if (user.Role == "Student")
                    {
                        return RedirectToAction("Index", "StudentDashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    ViewBag.Departments = await _context.Departments.ToListAsync();
                    ViewBag.Courses = await _context.Courses.ToListAsync();
                    return View(model);
                }

                // Check if email already exists in Students table
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.Email == model.Email);

                if (existingStudent != null)
                {
                    ModelState.AddModelError("Email", "Email already exists in student records.");
                    ViewBag.Departments = await _context.Departments.ToListAsync();
                    ViewBag.Courses = await _context.Courses.ToListAsync();
                    return View(model);
                }

                // Validate student-specific fields if role is Student
                if (model.Role == "Student")
                {
                    if (string.IsNullOrEmpty(model.PhoneNumber))
                    {
                        ModelState.AddModelError("PhoneNumber", "Phone number is required for students.");
                    }
                    if (string.IsNullOrEmpty(model.Address))
                    {
                        ModelState.AddModelError("Address", "Address is required for students.");
                    }
                    if (!model.DepartmentId.HasValue)
                    {
                        ModelState.AddModelError("DepartmentId", "Department is required for students.");
                    }
                    if (!model.CourseId.HasValue)
                    {
                        ModelState.AddModelError("CourseId", "Course is required for students.");
                    }

                    if (!ModelState.IsValid)
                    {
                        ViewBag.Departments = await _context.Departments.ToListAsync();
                        ViewBag.Courses = await _context.Courses.ToListAsync();
                        return View(model);
                    }
                }

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // Create User account
                    var user = new User
                    {
                        FullName = model.FullName,
                        Email = model.Email,
                        Password = HashPassword(model.Password),
                        Role = model.Role
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    // If role is Student, create Student record
                    if (model.Role == "Student")
                    {
                        var student = new Student
                        {
                            StudentName = model.FullName,
                            Email = model.Email,
                            PhoneNumber = model.PhoneNumber!,
                            Address = model.Address!,
                            DepartmentId = model.DepartmentId!.Value,
                            CourseId = model.CourseId!.Value
                        };

                        _context.Students.Add(student);
                        await _context.SaveChangesAsync();
                    }

                    await transaction.CommitAsync();
                    TempData["SuccessMessage"] = "Registration successful! Please login.";
                    return RedirectToAction("Login");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "An error occurred during registration. Please try again.");
                }
            }

            ViewBag.Departments = await _context.Departments.ToListAsync();
            ViewBag.Courses = await _context.Courses.ToListAsync();
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> GetCoursesByDepartment(int departmentId)
        {
            var courses = await _context.Courses
                .Where(c => c.DepartmentId == departmentId)
                .Select(c => new { c.CourseId, c.CourseName })
                .ToListAsync();

            return Json(courses);
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}