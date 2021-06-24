using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class UserMgtVM
    {
        public UserMgtVM()
        {
            UsersList = new List<UserVM>();

            AddUser = new UserVM();
        }
        public UserVM AddUser { get; set; }
        public List<UserVM> UsersList { get; set; }
    }

    public class ClassMgtVM
    {
        public ClassVM AddClass { get; set; }
        public List<ClassVM> ClassList { get; set; }
    }

    public class SubjectMgtVM
    {
        public SubjectMgtVM()
        {
            AddSubject = new SubjectVM();
            SubjectList = new List<SubjectVM>();
            TeachersList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
        }
        public SubjectVM AddSubject { get; set; }
        public List<SubjectVM> SubjectList { get; set; }
        public List<SelectListItem> TeachersList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
    }

    public class TestMgtVM
    {
        public TestVM AddTest { get; set; }
        public List<TestVM> TestList { get; set; }
    }
}
