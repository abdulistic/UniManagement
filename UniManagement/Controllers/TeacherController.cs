using Newtonsoft.Json;
using Restaurant.ClassLibrary.ViewModel;
using Restaurent.Models;
using Restaurent.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public async Task<ActionResult> TeacherManagement()
        {
            UserVM user = (UserVM)Session[WebUtil.CurrentUser];
            if (!(user != null && user.Role.Equals(WebUtil.Teacher))) return RedirectToAction("Login", "Users", new { returnUrl = "teacher/teachermanagement" });

            List<SubjectVM> userMgt = new List<SubjectVM>();
            userMgt = await service.GetSubjectList((int)user.UserId);

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



        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file)
        {
            int testId = Convert.ToInt32(TempData["TestId"]);

            try
            {
                if (Request.Files.Count > 0)
                {
                    var postedFile = Request.Files[0];
                    string temp = Path.GetTempPath();
                    string path = Path.Combine(temp, "Uploads");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    List<GradesListVM> grades = new List<GradesListVM>();

                    string fileName = Path.GetFileName(postedFile.FileName);
                    string filePath = Path.Combine(path, fileName);
                    postedFile.SaveAs(filePath);


                    string csvData = System.IO.File.ReadAllText(filePath);
                    DataTable dt = new DataTable();
                    bool firstRow = true;
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                if (firstRow)
                                {
                                    foreach (string cell in row.Split(','))
                                    {
                                        dt.Columns.Add(cell.Trim());
                                    }
                                    firstRow = false;
                                }
                                else
                                {
                                    dt.Rows.Add();
                                    int i = 0;
                                    foreach (string cell in row.Split(','))
                                    {
                                        dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                                        i++;
                                    }
                                }
                            }
                        }
                    }


                    foreach (DataRow row in dt.Rows)
                    {
                        if (row != null)
                        {
                            GradesListVM gradeRow = new GradesListVM();
                            gradeRow.StudentId = JsonConvert.DeserializeObject<int>(row.ItemArray[0].ToString());
                            gradeRow.StudentName = row.ItemArray[1].ToString();//JsonConvert.DeserializeObject<string>(row.ItemArray[1].ToString());
                            gradeRow.TestMarks = JsonConvert.DeserializeObject<int>(row.ItemArray[2].ToString());
                            gradeRow.TestId = testId;

                            grades.Add(gradeRow);
                        }
                    }

                    if (grades.Count > 0)
                    {
                        await service.AddResults(grades);
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("TestResults", new { id = testId });
        }
    }
}