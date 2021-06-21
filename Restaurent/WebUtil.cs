using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent
{
    public class WebUtil
    {
        public const string CURRENT_USER = "CurrentUser";
        public const int ADMIN_ROLE = 1;
        public const int CHEF_ROLE = 2;
        public const int PENDING_STATUS_ID = 1;
        public const string MY_COOKIE = "C217";
        public const string CurrentUser = "CurrentUser";
        public const string LoginCookie = "LoginCookie";
        public const int AdminRole = 1;
        public const string Cart_IdList = "CartIdList";
        public const string ProductDetail = "ProductDetail";
        public const string WaitingForReview = "Waiting for Review";
        public const string Accepted = "Accepted";
        public const string Delivered = "Delivered";
        public const string Declined = "Declined";
    }

    public enum ResponseStatus
    {
        OK = 200,
        Error = 400,
        Restrected = 403
    }

    public class Response
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
        public object ResultData { get; set; }
    }

    public static class AdminRoutes 
    {
        public const string BaseURL = "http://192.168.0.101/UniManagementApi/";

        public const string Start = BaseURL + "admin/start";
        public const string AddUser = BaseURL + "admin/AddUser";
        public const string AddClass = BaseURL + "admin/AddClass";
        public const string AddSubject = BaseURL + "admin/AddSubject";
        public const string GetUserList = BaseURL + "admin/GetUserList";
        public const string GetClassList = BaseURL + "admin/GetClassList";
        public const string GetSubjectList = BaseURL + "admin/GetSubjectList";
        public const string GetStudentList = BaseURL + "admin/GetStudentList";
        public const string GetUserById = BaseURL + "admin/GetUserById";
        public const string GetClassById = BaseURL + "admin/GetClassById";
        public const string GetSubjectById = BaseURL + "admin/GetSubjectById";
        public const string DeleteUserById = BaseURL + "admin/DeleteUserById";
        public const string DeleteClassById = BaseURL + "admin/DeleteClassById";
        public const string DeleteSubjectById = BaseURL + "admin/DeleteSubjectById";
        public const string DeassignClass = BaseURL + "admin/DeassignClass";
        public const string AssignClass = BaseURL + "admin/AssignClass";

    }

    public static class TeacherRoutes
    {
        public const string BaseURL = "http://192.168.0.101/UniManagementApi/teacher/";


        public const string GetSubjectList = BaseURL + "GetSubjectList";
        public const string AddTest = BaseURL + "AddTest";
        public const string GetTestById = BaseURL + "GetTestById";
        public const string DeleteTestById = BaseURL + "DeleteTestById";
        public const string GetTestList = BaseURL + "GetTestList";
        public const string GetTestResults = BaseURL + "GetTestResults";
        public const string AddTestResult = BaseURL + "AddTestResult";
        public const string GetStudentList = BaseURL + "GetStudentList";
        public const string GetStudentSubjects = BaseURL + "GetStudentSubjects";
        public const string GetTestResultsBySubjectId = BaseURL + "GetTestResultsBySubjectId";
    }
}