using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.ViewModels;

namespace StudentManagementSystem.Controllers
{
    public class StudentDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            // Get user information
            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Find student record by email
            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Email == user.Email);

            if (student == null)
            {
                ViewBag.Message = "Student profile not found. Please contact administrator to complete your profile setup.";
                ViewBag.UserEmail = user.Email;
                ViewBag.UserName = user.FullName;
                return View();
            }

            var viewModel = new StudentProfileViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                DepartmentName = student.Department.DepartmentName,
                CourseName = student.Course.CourseName,
                Duration = student.Course.Duration,
                Fees = student.Course.Fees
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            if (HttpContext.Session.GetString("UserRole") != "Student")
            {
                return RedirectToAction("Login", "Account");
            }

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.Users.FindAsync(int.Parse(userId));
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var student = await _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .FirstOrDefaultAsync(s => s.Email == user.Email);

            if (student == null)
            {
                return RedirectToAction("Index");
            }

            var viewModel = new StudentProfileViewModel
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                Address = student.Address,
                DepartmentName = student.Department.DepartmentName,
                CourseName = student.Course.CourseName,
                Duration = student.Course.Duration,
                Fees = student.Course.Fees
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile(StudentProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = await _context.Students.FindAsync(model.StudentId);
                if (student != null)
                {
                    student.PhoneNumber = model.PhoneNumber;
                    student.Address = model.Address;

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Profile updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}