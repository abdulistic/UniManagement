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
    public class StudentController : Controller
    {
        private readonly ITeacherService service;

        public StudentController()
        {
            this.service = new TeacherService();
        }

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> SubjectsList()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Student))) return RedirectToAction("Login", "Users", new { returnUrl = "student/SubjectsList" });

            List<SubjectVM> subjects = new List<SubjectVM>();
            subjects = await service.GetStudentSubjects((int)user.UserId);

            return View(subjects);
        }

        public async Task<ActionResult> SubjectTestResults(int subjectId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Student))) return RedirectToAction("Login", "Users", new { returnUrl = $"student/GetTestResultsBySubjectId?subjectId={subjectId}" });

            TestMgtVM testResults = new TestMgtVM();
            testResults = await service.GetTestResultsBySubjectId(subjectId, (int)user.UserId);

            return View(testResults);
        }


        public ActionResult Chat()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (user == null) return RedirectToAction("Login", "Users", new { returnUrl = "student/chat" });

            ViewBag.SubjectId = user.UserId;
            return View();
        }


        public async Task<JsonResult> GetChatUserList()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null)) return null;

            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatUserList((int)user.UserId);

            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetChatPeople()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null)) return null;

            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatPeople((int)user.UserId);

            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetChat(int id)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null)) return null;

            List<ChatVM> chatUsers = new List<ChatVM>();
            chatUsers = await service.GetChatRoomHistory(id, user.UserId);


            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddChatRoom(int id)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null)) return null;

            await service.AddChatRoom(new ChatInfoVM { SenderId = user.UserId, RecieverId = id });

            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatPeople((int)user.UserId);

            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddChat(ChatVM model)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null)) return null;

            model.SenderId = user.UserId;

            await service.AddChat(model);

            return Json("{'message':'Success'}", JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetChatCount()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null)) return null;

            int count = await service.GetNewChatCount((int)user.UserId);

            return Json(count, JsonRequestBehavior.AllowGet);
        }
    }
}