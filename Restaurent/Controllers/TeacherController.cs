using Restaurant.ClassLibrary.UsersMgt;
using Restaurant.ClassLibrary.ViewModel;
using Restaurent.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Restaurent.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService service;

        public TeacherController()
        {
            this.service = new TeacherService();
        }

        public ActionResult Index()
        {
            return Redirect("http://localhost/ChatApp/Account/Register");
        }

        public async  Task<ActionResult> Register()
        {
            string username = "abdulirehmankhan3333@gmail.com";
            string password = "Password1";
            await service.LoginForChat(username, password);

            return RedirectToAction("TeacherManagement");
        }

        public async Task<ActionResult> TeacherManagement()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            List<SubjectVM> userMgt = new List<SubjectVM>();
            userMgt = await service.GetSubjectList(3);

            return View(userMgt);
        }

        [HttpGet]
        public async Task<JsonResult> GetTestById(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return null;

            TestVM userVM = await service.GetTestById(id);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteTestById(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.DeleteTestById(id);

            return RedirectToAction("TeacherManagement");
        }

        public async Task<ActionResult> TestManagement(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/testmanagement" });

            TestMgtVM tests = new TestMgtVM();
            tests.TestList = await service.GetTestList(id);
            ViewBag.SubjectId = id;

            return View(tests);
        }

        [HttpPost]
        public async Task<ActionResult> AddTest(TestMgtVM data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AddTest(data.AddTest);

            return RedirectToAction("TeacherManagement");
        }

        [HttpPost]
        public async Task<ActionResult> AddTestResult(TestMgtVM data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AddTestResult(data.AddTest);

            return RedirectToAction("TeacherManagement");
        }


        public async Task<ActionResult> TestResults(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/testmanagement" });

            TestMgtVM tests = new TestMgtVM();
            tests = await service.GetTestResults(id);
            ViewBag.SubjectId = id;

            return View(tests);
        }

        public async Task<ActionResult> StudentList(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/testmanagement" });

            TestMgtVM tests = new TestMgtVM();
            tests = await service.GetStudentList(id);

            return View(tests);
        }


    }
}