using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CourseController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            var courses = await _context.Courses
                .Include(c => c.Department)
                .ToListAsync();
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course created successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Course updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", course.DepartmentId);
            return View(course);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                // Check if course has students
                var hasStudents = await _context.Students.AnyAsync(s => s.CourseId == id);

                if (hasStudents)
                {
                    TempData["ErrorMessage"] = "Cannot delete course. It has associated students.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Course deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.CourseId == id);
        }
    }
}