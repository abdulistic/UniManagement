using Newtonsoft.Json;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Restaurent.Service
{
    public interface IAdminService
    {
        Task<Response> AddUser(UserVM model);
        Task<Response> AddClass(ClassVM model);
        Task<Response> AddSubject(SubjectVM model);
        Task<List<UserVM>> GetUserList(string token);
        Task<List<ClassVM>> GetClassList();
        Task<SubjectMgtVM> GetSubjectList();
        Task<SubjectMgtVM> GetStudentList();
        Task<UserVM> GetUserById(long userId);
        Task<ClassVM> GetClassById(long classId);
        Task<SubjectVM> GetSubjectById(long subjectId);
        Task<Response> DeleteUserById(long userId);
        Task<Response> DeleteClassById(long classId);
        Task<Response> DeleteSubjectById(long subjectId);
        Task<Response> AssignClass(SubjectVM model);
        Task<Response> DeassignClass(long userId);
        Task<UserVM> GetUser(UserVM model);
    }

    public class AdminService : IAdminService
    {
        private readonly IHttpClientService httpClient;
        public AdminService()
        {
            this.httpClient = new HttpClientService();
        }
        public async Task<Response> AddUser(UserVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{AdminRoutes.AddUser}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<UserVM>> GetUserList(string token)
        {
            List<UserVM> response = new List<UserVM>();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetUserList}", token);
                response = JsonConvert.DeserializeObject<List<UserVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<ClassVM>> GetClassList()
        {
            List<ClassVM> response = new List<ClassVM>();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetClassList}");
                response = JsonConvert.DeserializeObject<List<ClassVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<UserVM> GetUserById(long userId)
        {
            UserVM response = new UserVM();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetUserById}?userId={userId}");
                response = JsonConvert.DeserializeObject<UserVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> DeleteUserById(long userId)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.DeleteUserById}?userId={userId}");
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AddClass(ClassVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{AdminRoutes.AddClass}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<ClassVM> GetClassById(long classId)
        {
            ClassVM response = new ClassVM();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetClassById}?classId={classId}");
                response = JsonConvert.DeserializeObject<ClassVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> DeleteClassById(long classId)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.DeleteClassById}?classId={classId}");
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AddSubject(SubjectVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{AdminRoutes.AddSubject}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<SubjectMgtVM> GetSubjectList()
        {
            SubjectMgtVM response = new SubjectMgtVM();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetSubjectList}");
                response = JsonConvert.DeserializeObject<SubjectMgtVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<SubjectVM> GetSubjectById(long subjectId)
        {
            SubjectVM response = new SubjectVM();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetSubjectById}?subjectId={subjectId}");
                response = JsonConvert.DeserializeObject<SubjectVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> DeleteSubjectById(long subjectId)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.DeleteSubjectById}?subjectId={subjectId}");
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<SubjectMgtVM> GetStudentList()
        {
            SubjectMgtVM response = new SubjectMgtVM();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.GetStudentList}");
                response = JsonConvert.DeserializeObject<SubjectMgtVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AssignClass(SubjectVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{AdminRoutes.AssignClass}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> DeassignClass(long userId)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.GetAsync($"{AdminRoutes.DeassignClass}?userId={userId}");
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<UserVM> GetUser(UserVM model)
        {
            UserVM response = new UserVM();
            try
            {
                string json = await httpClient.PostAsync($"{AdminRoutes.GetUser}", model);
                response = JsonConvert.DeserializeObject<UserVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}