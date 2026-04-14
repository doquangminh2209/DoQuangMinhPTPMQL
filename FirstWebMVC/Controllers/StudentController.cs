using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;
using FirstWebMVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ================= INDEX =================
        public async Task<IActionResult> Index()
        {
            var danhSachSinhVien = await _context.Students
                .Include(s => s.Faculty)
                .Select(s => new StudentFacultyViewModel
                {
                    StudentCode = s.StudentCode,
                    FullName = s.FullName,
                    Age = s.Age,
                    Email = s.Email,
                    FacultyName = s.Faculty != null ? s.Faculty.FacultyName : "Chưa có khoa"
                })
                .ToListAsync();

            return View(danhSachSinhVien);
        }

        // ================= CREATE =================
        [HttpGet]
        public IActionResult Create()
        {
            var danhSachKhoa = _context.Faculties.ToList();
            ViewBag.FacultyList = new SelectList(danhSachKhoa, "FacultyID", "FacultyName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            ModelState.Remove("Faculty");

            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var danhSachKhoa = _context.Faculties.ToList();
            ViewBag.FacultyList = new SelectList(danhSachKhoa, "FacultyID", "FacultyName", student.FacultyID);

            return View(student);
        }

        // ================= EDIT =================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return View("NotFound");

            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return View("NotFound");

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            if (id != student.StudentCode)
                return View("NotFound");

            if (ModelState.IsValid)
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(student);
        }

        // ================= DELETE =================
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return View("NotFound");

            var student = await _context.Students.FindAsync(id);

            if (student == null)
                return View("NotFound");

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}