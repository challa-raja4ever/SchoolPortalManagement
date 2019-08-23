using System.Collections.Generic;

namespace TeacherStudentPortal.Models
{
    public class StudentViewModel
    {
        public string Name { get; set; }
        public int StudentId { get; set; }

        public List<CoursesList> CoursesList { get; set; }
        public List<CoursesGrades> CourseGrades { get; set; }
    }
}