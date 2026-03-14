using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;

namespace StudentManagementSystem.Controllers
{
    public class TeacherDashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TeacherDashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            ViewBag.TotalDepartments = await _context.Departments.CountAsync();
            ViewBag.TotalCourses = await _context.Courses.CountAsync();
            ViewBag.TotalStudents = await _context.Students.CountAsync();

            // Get students per department
            var studentsPerDepartment = await _context.Students
                .Include(s => s.Department)
                .GroupBy(s => s.Department.DepartmentName)
                .Select(g => new { Department = g.Key, Count = g.Count() })
                .ToListAsync();

            ViewBag.StudentsPerDepartment = studentsPerDepartment;

            return View();
        }
    }
}