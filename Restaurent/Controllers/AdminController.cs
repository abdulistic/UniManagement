using Restaurant.ClassLibrary.PakClassified;
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
    public class AdminController : Controller
    {
        private readonly IAdminService service;

        public AdminController()
        {
            this.service = new AdminService();
        }


        // GET: Admin
        public ActionResult DashBoard()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            return View();
        }

        public async Task<ActionResult> UserManagement()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });
            UserMgtVM userMgt = new UserMgtVM();
            userMgt.UsersList = await service.GetUserList();

            return View(userMgt);
        }

        public ActionResult Test()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserMgtVM data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AddUser(data.AddUser);

            return RedirectToAction("UserManagement");
        }

        [HttpGet]
        public async Task<JsonResult> GetUserById(long userId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return null;

            UserVM userVM = await service.GetUserById(userId);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteUserById(long userId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.DeleteUserById(userId);

            return RedirectToAction("UserManagement");
        }

        public async Task<ActionResult> ClassManagement()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });
            ClassMgtVM classMgt = new ClassMgtVM();
            classMgt.ClassList = await service.GetClassList();

            return View(classMgt);
        }


        [HttpPost]
        public async Task<ActionResult> AddClass(ClassMgtVM data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AddClass(data.AddClass);

            return RedirectToAction("ClassManagement");
        }

        [HttpGet]
        public async Task<JsonResult> GetClassById(long classId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return null;

            ClassVM userVM = await service.GetClassById(classId);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteClassById(long classId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.DeleteClassById(classId);

            return RedirectToAction("ClassManagement");
        }

        public async Task<ActionResult> SubjectManagement()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });
            SubjectMgtVM subjectMgt = new SubjectMgtVM();
            subjectMgt = await service.GetSubjectList();


            return View(subjectMgt);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubject(SubjectMgtVM data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AddSubject(data.AddSubject);

            return RedirectToAction("SubjectManagement");
        }

        [HttpGet]
        public async Task<JsonResult> GetSubjectById(long subjectId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return null;

            SubjectVM userVM = await service.GetSubjectById(subjectId);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSubjectById(long subjectId)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.DeleteSubjectById(subjectId);

            return RedirectToAction("SubjectManagement");
        }

        public async Task<ActionResult> StudentManagement()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });
            SubjectMgtVM subjectMgt = new SubjectMgtVM();
            subjectMgt = await service.GetStudentList();


            return View(subjectMgt);
        }

        [HttpPost]
        public async Task<ActionResult> AssignClass(SubjectMgtVM data)
        {
            //User user = (User)Session[WebUtil.CurrentUser];
            //if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.AssignClass(data.AddSubject);

            return RedirectToAction("StudentManagement");
        }

        [HttpGet]
        public async Task<ActionResult> DeassignClass(int userId)
        {
            //User user = (User)Session[WebUtil.CurrentUser];
            //if (!(user != null && user.IsInRole(WebUtil.AdminRole))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/dashBoard" });

            await service.DeassignClass(userId);

            return RedirectToAction("StudentManagement");
        }
    }
}