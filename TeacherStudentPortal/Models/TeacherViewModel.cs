using System.Collections.Generic;

namespace TeacherStudentPortal.Models
{
    public class TeacherViewModel
    {
        public string Name { get; set; }
        public int CourseId { get; set; }
        public List<Student> Students { get; set; }
    }
}