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
            return View();
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

            return RedirectToAction("TestManagement");
        }

        public async Task<ActionResult> TestManagement(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            List<TestVM> tests = new List<TestVM>();
            tests = await service.GetTestList(id);

            return View(tests);
        }

        [HttpPost]
        public async Task<ActionResult> AddTest(TestVM data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AddTest(data);

            return RedirectToAction("TestManagement");
        }
    }
}