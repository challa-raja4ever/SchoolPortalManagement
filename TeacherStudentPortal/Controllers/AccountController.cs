using System.Web.Mvc;
using TeacherStudentPortal.Models;

namespace TeacherStudentPortal.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var objUtilities = new Utilities.Utilities();
                var result = objUtilities.RegisterAdmin(model);
                if (result)
                {
                    return RedirectToAction("RegisterSuccess", "Home");
                }
            }

            return View(model);
        }

        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //GET : /Account/LogOut
        [AllowAnonymous]
        public ActionResult LogOut(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return RedirectToAction("Default", "Home");
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var objUtilities = new Utilities.Utilities();
            var validateAdminLogin = objUtilities.ValidateAdminLogin(model);
            if (validateAdminLogin)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Account/TeacherLogin
        [AllowAnonymous]
        public ActionResult TeacherLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/TeacherLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult TeacherLogin(LoginViewModel model, string returnUrl)
        {
            var objUtilities = new Utilities.Utilities();
            var validateTeacherLogin = objUtilities.ValidateTeacherLogin(model);
            if (validateTeacherLogin)
            {
                var courseIdTaughtByTeacher = objUtilities.FetchCourseIdTaughtByTeacher(model.Username);
                return RedirectToAction("TeacherHome", "Home", new { courseId  = courseIdTaughtByTeacher });
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }

        // GET: /Account/StudentLogin
        [AllowAnonymous]
        public ActionResult StudentLogin(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/TeacherLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult StudentLogin(LoginViewModel model, string returnUrl)
        {
            var objUtilities = new Utilities.Utilities();
            var validateStudentLogin = objUtilities.ValidateStudentLogin(model);
            if (validateStudentLogin)
            {
                var currentStudentId = objUtilities.FetchStudentId(model.Username);
                return RedirectToAction("StudentHome", "Home", new { studentId = currentStudentId });
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View(model);
        }
    }
}