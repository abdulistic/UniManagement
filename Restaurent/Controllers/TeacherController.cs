using Restaurant.ClassLibrary.ViewModel;
using Restaurent.Models;
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

        public async Task<ActionResult> Register()
        {
            string username = "abdulirehmankhan3333@gmail.com";
            string password = "Password1";
            await service.LoginForChat(username, password);

            return RedirectToAction("TeacherManagement");
        }

        public async Task<ActionResult> TeacherManagement()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            List<SubjectVM> userMgt = new List<SubjectVM>();
            userMgt = await service.GetSubjectList(3);

            return View(userMgt);
        }

        [HttpGet]
        public async Task<JsonResult> GetTestById(int id)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return null;

            TestVM userVM = await service.GetTestById(id);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteTestById(int id, int subjectId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            Response response = await service.DeleteTestById(id);

            if (response.Status == ResponseStatus.OK) // Is User Input Valid?
            {
                TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-success", Title = "Success!", Message = response.Message };
            }
            else
            {
                TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-danger", Title = "Error!", Message = response.Message };
            }

            return RedirectToAction("TestManagement", new { id = subjectId });
        }

        public async Task<ActionResult> TestManagement(int id)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/testmanagement" });

            TestMgtVM tests = new TestMgtVM();
            tests.TestList = await service.GetTestList(id);
            ViewBag.SubjectId = id;

            return View(tests);
        }

        [HttpPost]
        public async Task<ActionResult> AddTest(TestMgtVM data)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            Response response = await service.AddTest(data.AddTest);

            if (response.Status == ResponseStatus.OK) // Is User Input Valid?
            {
                TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-success", Title = "Success!", Message = response.Message };
            }
            else
            {
                TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-danger", Title = "Error!", Message = response.Message };
            }

            return RedirectToAction("TestManagement", new { id = data.AddTest.SubjectId });
        }

        [HttpPost]
        public async Task<ActionResult> AddTestResult(TestMgtVM data)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            await service.AddTestResult(data.AddTest);

            return RedirectToAction("TeacherManagement");
        }


        public async Task<ActionResult> TestResults(int id)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            TestMgtVM tests = new TestMgtVM();
            tests = await service.GetTestResults(id);
            ViewBag.SubjectId = id;

            return View(tests);
        }

        public async Task<ActionResult> StudentList()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            TestMgtVM tests = new TestMgtVM();
            tests = await service.GetStudentList((int)user.UserId);

            return View(tests);
        }

        public async Task<ActionResult> GetChatUserList()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });


            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatUserList((int)user.UserId);

            return View(chatUsers);
        }


    }
}