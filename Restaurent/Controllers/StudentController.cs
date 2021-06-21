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

            return View();
        }
    }
}