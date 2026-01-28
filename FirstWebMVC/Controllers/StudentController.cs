using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models.Entities; // Quan trọng: Gọi đúng namespace chứa Student

namespace FirstWebMVC.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            // Chuyển hướng ngay lập tức sang trang Create
            return RedirectToAction("Create");
        }
        
        // 1. Action GET: Hiển thị form nhập liệu khi người dùng truy cập
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 2. Action POST: Nhận dữ liệu khi người dùng bấm nút Gửi
        [HttpPost]
        public IActionResult Create(Student std)
        {
            // Kiểm tra xem dữ liệu có nhận được không
            string message = "";
            
            if (std != null) 
            {
                message = "Đã nhận thành công: " + std.StudentCode + " - " + std.FullName;
            }
            else 
            {
                message = "Không nhận được dữ liệu!";
            }
            
            // Gửi thông báo ngược lại View để hiển thị
            ViewBag.ThongBao = message;
            
            return View();
        }
    }
}