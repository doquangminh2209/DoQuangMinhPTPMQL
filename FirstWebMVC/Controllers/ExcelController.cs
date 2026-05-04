using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using FirstWebMVC.Data;
using FirstWebMVC.Models.Entities;

namespace FirstWebMVC.Controllers
{
    public class ExcelController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExcelController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Content("File không hợp lệ");

            // ✅ ĐÚNG CHUẨN EPPlus 8
            ExcelPackage.License.SetNonCommercialPersonal("Minh");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    int rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var student = new Student
                        {
                            StudentCode = worksheet.Cells[row, 1].Text,
                            FullName = worksheet.Cells[row, 2].Text,
                            Age = int.TryParse(worksheet.Cells[row, 3].Text, out int age) ? age : 0,
                            Email = worksheet.Cells[row, 4].Text,
                            FacultyID = int.TryParse(worksheet.Cells[row, 5].Text, out int fid) ? fid : 0
                        };

                        _context.Students.Add(student);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return Content("Upload thành công!");
        }
    }
}