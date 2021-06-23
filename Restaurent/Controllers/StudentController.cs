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
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "student/SubjectsList" });

            List<SubjectVM> subjects = new List<SubjectVM>();
            subjects = await service.GetStudentSubjects(4);

            return View(subjects);
        }

        public async Task<ActionResult> SubjectTestResults(int subjectId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = $"student/GetTestResultsBySubjectId?subjectId={subjectId}" });

            TestMgtVM testResults = new TestMgtVM();
            testResults = await service.GetTestResultsBySubjectId(subjectId, 4);

            return View(testResults);
        }


        public ActionResult Chat()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "student/chat" });
            ViewBag.SubjectId = 4;


            return View();
        }


        public async Task<JsonResult> GetChatUserList()
        {
            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatUserList(7);

            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }


        public async Task<JsonResult> GetChatPeople()
        {
            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatPeople(7);

            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetChat(int id)
        {
            List<ChatVM> chatUsers = new List<ChatVM>();
            chatUsers = await service.GetChatRoomHistory(id);


            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> AddChatRoom(int id)
        {
            await service.AddChatRoom(new ChatInfoVM { SenderId = 7, RecieverId = id });

            List<UserVM> chatUsers = new List<UserVM>();
            chatUsers = await service.GetChatPeople(7);

            return Json(chatUsers, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> AddChat(ChatVM model)
        {
            model.SenderId = 7;

            await service.AddChat(model);

            return Json("{'message':'Success'}", JsonRequestBehavior.AllowGet);
        }
    }
}