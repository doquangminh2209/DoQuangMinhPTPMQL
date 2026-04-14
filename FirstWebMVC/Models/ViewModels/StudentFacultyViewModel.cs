namespace FirstWebMVC.Models.ViewModels
{
    public class StudentFacultyViewModel
    {
        public string StudentCode { get; set; }
        public string FullName { get; set; }

        public int Age { get; set; }          // thêm
        public string Email { get; set; }     // thêm

        public string FacultyName { get; set; }
    }
}