using System.Web.Mvc;
using TeacherStudentPortal.Models;

namespace TeacherStudentPortal.Controllers
{
    public class HomeController : Controller
    {
        //Default Page
        public ActionResult Default()
        {
            return View();
        }

        //Registration Successful page of Admin
        public ActionResult RegisterSuccess()
        {
            return View();
        }

        //Index page for the Admin Role
        public ActionResult Index()
        {
            var objUtilities = new Utilities.Utilities();
            AdminViewModel model = objUtilities.GetStudentsAndTeachersList();
            return View(model);
        }

        //Index page for the Teacher Role
        public ActionResult TeacherHome(int courseId)
        {
            var objUtilities = new Utilities.Utilities();
            var model = objUtilities.GetStudentsList();
            model.CourseId = courseId;
            model.Name = objUtilities.GetTeacherName(courseId);
            return View(model);
        }

        //Index page for the Student Role
        public ActionResult StudentHome(int studentId)
        {
            var objUtilities = new Utilities.Utilities();
            StudentViewModel model = objUtilities.GetCourseGrades(studentId);
            model.StudentId = studentId;
            model.Name = objUtilities.GetStudentName(studentId);
            return View(model);
        }

        // GET: /Home/AddTeacher
        [AllowAnonymous]
        public ActionResult AddTeacher(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Home/AddTeacher
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddTeacher(AddTeacherModel model, string returnUrl)
        {
            var objUtilities = new Utilities.Utilities();
            bool validateInsertTeacherRecord = objUtilities.InsertTeacherRecord(model);
            if (validateInsertTeacherRecord)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "UserName or CourseId Already exists.");
            return View();
        }


        // GET: /Home/AddStudent
        [AllowAnonymous]
        public ActionResult AddStudent(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Home/AddStudent
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult AddStudent(AddStudentModel model, string returnUrl)
        {
            var objUtilities = new Utilities.Utilities();
            bool validateInsertTeacherRecord = objUtilities.InsertStudentRecord(model);
            if (validateInsertTeacherRecord)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "UserName or StudentId Already exists.");
            return View();
        }

        // GET: /Home/AssignGrades
        [AllowAnonymous]
        public ActionResult AssignGrades(string returnUrl, int courseId, string name)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.courseId = courseId;
            ViewBag.TeacherName = name;
            return View();
        }

        // POST: /Home/AssignGrades
        [HttpPost]
        [AllowAnonymous]
        public ActionResult AssignGrades(AssignGradeModel model, int CourseId)
        {
            var objUtilities = new Utilities.Utilities();
            var validateAssignGrade= objUtilities.AssignGrade(model);
            if (validateAssignGrade)
            {
                return RedirectToAction("TeacherHome", "Home", new { courseId = CourseId } );
            }
            ModelState.AddModelError("", "Enter Valid StudentId.");
            return View();
        }
    }
}