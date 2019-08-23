using System.Collections.Generic;

namespace TeacherStudentPortal.Models
{
    public class AdminViewModel
    {
        public List<TeacherCourse> TeachersList { get; set; }
        public List<Student> StudentList { get; set; }
    }
}