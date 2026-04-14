using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWebMVC.Models.Entities
{
    [Table("Students")]
    public class Student
    {
        [Key]
        [Required(ErrorMessage = "Mã sinh viên không được để trống!")]
        public string StudentCode { get; set; }

        [Required(ErrorMessage = "Họ và tên không được để trống!")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Họ tên phải từ 3 đến 50 ký tự!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tuổi!")]
        [Range(18, 200, ErrorMessage = "Yêu cầu trên 18 tuổi!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email!")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng!")]
        public string Email { get; set; }

        public string FacultyID { get; set; }

        [ForeignKey("FacultyID")]
        public virtual Faculty Faculty { get; set; }
    }
}