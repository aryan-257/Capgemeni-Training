using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString, int? departmentFilter)
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            var students = _context.Students
                .Include(s => s.Department)
                .Include(s => s.Course)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.StudentName.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            if (departmentFilter.HasValue)
            {
                students = students.Where(s => s.DepartmentId == departmentFilter);
                ViewData["DepartmentFilter"] = departmentFilter;
            }

            ViewData["Departments"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName");

            // Get users with Student role who don't have student records
            var usersWithoutStudentRecords = await _context.Users
                .Where(u => u.Role == "Student" && !_context.Students.Any(s => s.Email == u.Email))
                .ToListAsync();
            
            ViewBag.UsersWithoutStudentRecords = usersWithoutStudentRecords;

            return View(await students.ToListAsync());
        }

        public async Task<IActionResult> Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName");
            ViewData["CourseId"] = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.Email == student.Email);

                if (existingStudent != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }
                else
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewData["CourseId"] = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName", student.CourseId);
            return View(student);
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

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewData["CourseId"] = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName", student.CourseId);
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Check if email already exists for other students
                    var existingStudent = await _context.Students
                        .FirstOrDefaultAsync(s => s.Email == student.Email && s.StudentId != student.StudentId);

                    if (existingStudent != null)
                    {
                        ModelState.AddModelError("Email", "Email already exists.");
                    }
                    else
                    {
                        _context.Update(student);
                        await _context.SaveChangesAsync();
                        TempData["SuccessMessage"] = "Student updated successfully!";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewData["CourseId"] = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName", student.CourseId);
            return View(student);
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

            var student = await _context.Students
                .Include(s => s.Course)
                .Include(s => s.Department)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Student deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
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

        [HttpGet]
        public async Task<IActionResult> CreateFromUser(int userId)
        {
            if (HttpContext.Session.GetString("UserRole") != "Teacher")
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null || user.Role != "Student")
            {
                return NotFound();
            }

            // Check if student record already exists
            var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Email == user.Email);
            if (existingStudent != null)
            {
                TempData["ErrorMessage"] = "Student record already exists for this user.";
                return RedirectToAction("Index");
            }

            var student = new Student
            {
                StudentName = user.FullName,
                Email = user.Email,
                PhoneNumber = "", // Will be filled by user
                Address = "" // Will be filled by user
            };

            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName");
            ViewData["CourseId"] = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName");
            ViewBag.UserName = user.FullName;
            ViewBag.UserEmail = user.Email;
            
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFromUser(Student student)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingStudent = await _context.Students
                    .FirstOrDefaultAsync(s => s.Email == student.Email);

                if (existingStudent != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                }
                else
                {
                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Student profile created successfully!";
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewData["DepartmentId"] = new SelectList(await _context.Departments.ToListAsync(), "DepartmentId", "DepartmentName", student.DepartmentId);
            ViewData["CourseId"] = new SelectList(await _context.Courses.ToListAsync(), "CourseId", "CourseName", student.CourseId);
            return View(student);
        }
        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}