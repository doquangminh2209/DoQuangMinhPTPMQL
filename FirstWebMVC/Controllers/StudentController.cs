using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models.Entities; // Quan trọng: Gọi đúng namespace chứa Student
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;
using System.Threading.Tasks;

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            // Chuyển hướng ngay lập tức sang trang Create
            return RedirectToAction("Create");
             _context = context;
        }
        
        // 1. Action GET: Hiển thị form nhập liệu khi người dùng truy cập
        // --- YÊU CẦU 2: HIỂN THỊ DỮ LIỆU ---
        public async Task<IActionResult> Index()
        {
            var danhSachSinhVien = await _context.Students.ToListAsync();
            return View(danhSachSinhVien);
        }

        // --- YÊU CẦU 3: THÊM MỚI DỮ LIỆU (CREATE) ---
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 2. Action POST: Nhận dữ liệu khi người dùng bấm nút Gửi
        [HttpPost]
        public IActionResult Create(Student std)
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // 1. Action GET: Lấy dữ liệu của bản ghi muốn sửa => trả dữ liệu về View
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            // Kiểm tra xem dữ liệu có nhận được không
            string message = "";
            if (id == null)
            {
                return NotFound(); // Báo lỗi nếu không có mã sinh viên
            }

            // Tìm sinh viên trong CSDL dựa vào khóa chính (StudentCode)
            var student = await _context.Students.FindAsync(id);
            
            if (std != null) 
            if (student == null)
            {
                message = "Đã nhận thành công: " + std.StudentCode + " - " + std.FullName;
                return NotFound(); // Báo lỗi nếu không tìm thấy sinh viên trong CSDL
            }
            else 
             // Trả dữ liệu của sinh viên đó về View Edit để hiển thị lên Form
            return View(student);
        }

        // 3. Nhận dữ liệu từ view gửi lên và tiến hành lưu vào CSDL (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Student student)
        {
            // Kiểm tra xem mã sinh viên trên đường dẫn (id) và trong Form (student.StudentCode) có khớp nhau không
            if (id != student.StudentCode)
            {
                message = "Không nhận được dữ liệu!";
                 return NotFound();
            }

            // Kiểm tra tính hợp lệ của dữ liệu
            if (ModelState.IsValid)
            {
                // Cập nhật thông tin mới vào DbContext
                _context.Update(student);
                
                // Lưu thay đổi vào CSDL (file App.db)
                await _context.SaveChangesAsync();
                
                // Lưu xong thì tự động quay về trang danh sách (Index)
                return RedirectToAction(nameof(Index));
            }
            
            // Gửi thông báo ngược lại View để hiển thị
            ViewBag.ThongBao = message;
            // Nếu dữ liệu lỗi, hiển thị lại Form với thông tin vừa nhập
            return View(student);
        }

        // 1. Action GET: Lấy dữ liệu của bản ghi muốn xoá => trả dữ liệu về View
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound(); // Báo lỗi nếu không nhận được mã sinh viên
            }

            // Tìm bản ghi trong CSDL
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // Trả dữ liệu về View để hiển thị form xác nhận
            return View(student); 
        }

        // 3. Submit for delete => Xoá bỏ khỏi dbContext => Lưu thay đổi vào CSDL
        // Lưu ý: Đặt tên hàm là DeleteConfirmed nhưng vẫn map với action "Delete" trên Form
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            // Tìm lại bản ghi cần xóa
            var student = await _context.Students.FindAsync(id);
            
            return View();
            if (student != null)
            {
                // Xóa bỏ khỏi dbContext
                _context.Students.Remove(student); 
                
                // Lưu thay đổi vào CSDL (App.db)
                await _context.SaveChangesAsync(); 
            }
            
            // Xóa xong thì quay về trang danh sách
            return RedirectToAction(nameof(Index)); 
        }
    }
}