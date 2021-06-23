using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniManagementApi.Services;
using UniPortalModel;
using static UniManagementApi.SupportClasses.Utility;

namespace UniManagementApi.Controllers
{
    [Produces("application/json")]
    public class AdminController : Controller
    {
        private readonly IAdminService service;

        public AdminController(IAdminService service)
        {
            this.service = service;
        }

        [HttpGet]
        public string Start()
        {
            return "Api has been started";
        }

        [HttpPost]
        public async Task<Response> AddUser([FromBody] UserVM model)
        {
            return await service.AddUser(model);
        }

        [HttpPost]
        public async Task<Response> AddClass([FromBody] ClassVM model)
        {
            return await service.AddClass(model);
        }

        [HttpPost]
        public async Task<Response> AddSubject([FromBody] SubjectVM model)
        {
            return await service.AddSubject(model);
        }

        [HttpPost]
        public async Task<Response> AssignClass([FromBody] SubjectVM model)
        {
            return await service.AssignClass(model);
        }

        
        
        [HttpGet]
        public async Task<List<UserVM>> GetUserList()
        {
            return await service.GetUserList();
        }

        [HttpGet]
        public async Task<List<ClassVM>> GetClassList()
        {
            return await service.GetClassList();
        }

        [HttpGet]
        public async Task<SubjectMgtVM> GetSubjectList()
        {
            return await service.GetSubjectList();
        }

        [HttpGet]
        public async Task<SubjectMgtVM> GetStudentList()
        {
            return await service.GetStudentList();
        }



        [HttpGet]
        public async Task<UserVM> GetUserById(long userId)
        {
            return await service.GetUserById(userId);
        }

        [HttpGet]
        public async Task<ClassVM> GetClassById(long classId)
        {
            return await service.GetClassById(classId);
        }

        [HttpGet]
        public async Task<SubjectVM> GetSubjectById(long subjectId)
        {
            return await service.GetSubjectById(subjectId);
        }



        [HttpGet]
        public async Task<Response> DeleteUserById(long userId)
        {
            return await service.DeleteUserById(userId);
        }

        [HttpGet]
        public async Task<Response> DeleteClassById(long classId)
        {
            return await service.DeleteClassById(classId);
        }

        [HttpGet]
        public async Task<Response> DeleteSubjectById(long subjectId)
        {
            return await service.DeleteSubjectById(subjectId);
        }

        [HttpGet]
        public async Task<Response> DeassignClass(long userId)
        {
            return await service.DeassignClass(userId);
        }

        [HttpPost]
        public async Task<UserVM> GetUser([FromBody]UserVM model)
        {
            return await service.GetUser(model);
        }

    }
}
