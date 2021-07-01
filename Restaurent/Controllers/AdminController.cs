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
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            return View();
        }

        public async Task<ActionResult> UserManagement()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });
            UserMgtVM userMgt = new UserMgtVM();
            userMgt = new UserMgtVM();

            userMgt.UsersList = await service.GetUserList(user.Token);

            return View(userMgt);
        }

        public ActionResult Test()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserMgtVM data)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.AddUser(data.AddUser);

            return RedirectToAction("UserManagement");
        }

        [HttpGet]
        public async Task<JsonResult> GetUserById(long userId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return null;

            UserVM userVM = await service.GetUserById(userId);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteUserById(long userId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.DeleteUserById(userId);

            return RedirectToAction("UserManagement");
        }

        public async Task<ActionResult> ClassManagement()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });
            ClassMgtVM classMgt = new ClassMgtVM();
            classMgt.ClassList = await service.GetClassList();

            return View(classMgt);
        }


        [HttpPost]
        public async Task<ActionResult> AddClass(ClassMgtVM data)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.AddClass(data.AddClass);

            return RedirectToAction("ClassManagement");
        }

        [HttpGet]
        public async Task<JsonResult> GetClassById(long classId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return null;

            ClassVM userVM = await service.GetClassById(classId);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteClassById(long classId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.DeleteClassById(classId);

            return RedirectToAction("ClassManagement");
        }

        public async Task<ActionResult> SubjectManagement()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });
            SubjectMgtVM subjectMgt = new SubjectMgtVM();
            subjectMgt = await service.GetSubjectList();


            return View(subjectMgt);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubject(SubjectMgtVM data)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.AddSubject(data.AddSubject);

            return RedirectToAction("SubjectManagement");
        }

        [HttpGet]
        public async Task<JsonResult> GetSubjectById(long subjectId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return null;

            SubjectVM userVM = await service.GetSubjectById(subjectId);

            return Json(userVM, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteSubjectById(long subjectId)
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.DeleteSubjectById(subjectId);

            return RedirectToAction("SubjectManagement");
        }

        public async Task<ActionResult> StudentManagement()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });
            SubjectMgtVM subjectMgt = new SubjectMgtVM();
            subjectMgt = await service.GetStudentList();


            return View(subjectMgt);
        }

        [HttpPost]
        public async Task<ActionResult> AssignClass(SubjectMgtVM data)
        {
            //UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            //if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.AssignClass(data.AddSubject);

            return RedirectToAction("StudentManagement");
        }

        [HttpGet]
        public async Task<ActionResult> DeassignClass(int userId)
        {
            //UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            //if (!(user != null && user.Role.Equals(WebUtil.Admin))) return RedirectToAction("Login", "Users", new { returnUrl = "admin/usermanagement" });

            await service.DeassignClass(userId);

            return RedirectToAction("StudentManagement");
        }
    }
}