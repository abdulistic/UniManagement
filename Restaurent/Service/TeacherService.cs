using Newtonsoft.Json;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Restaurent.Service
{
    public interface ITeacherService
    {
        Task<List<SubjectVM>> GetSubjectList(int id);
        Task<Response> DeleteTestById(int id);
        Task<Response> AddTest(TestVM model);
        Task<TestVM> GetTestById(int id);
        Task<List<TestVM>> GetTestList(int id);
        Task<TestMgtVM> GetTestResults(int id);
        Task<TestMgtVM> GetStudentList(int id);
        Task<Response> AddTestResult(TestVM model);
        Task<List<SubjectVM>> GetStudentSubjects(int id);
        Task<TestMgtVM> GetTestResultsBySubjectId(int subjectId, int studentId);
        Task LoginForChat(string username, string password);
        Task<List<UserVM>> GetChatUserList(int id);
        Task<Response> AddChat(ChatVM model);
        Task<Response> AddChatRoom(ChatInfoVM model);
        Task<List<UserVM>> GetChatPeople(int id);
        Task<List<ChatVM>> GetChatRoomHistory(int id, long userId);
        Task<int> GetNewChatCount(int id);
    }

    public class TeacherService : ITeacherService
    {
        private readonly IHttpClientService httpClient;
        public TeacherService()
        {
            this.httpClient = new HttpClientService();
        }

        public async Task<List<SubjectVM>> GetSubjectList(int id)
        {
            List<SubjectVM> response = new List<SubjectVM>();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetSubjectList}?id={id}");
                response = JsonConvert.DeserializeObject<List<SubjectVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<TestVM>> GetTestList(int id)
        {
            List<TestVM> response = new List<TestVM>();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetTestList}?id={id}");
                response = JsonConvert.DeserializeObject<List<TestVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<TestMgtVM> GetTestResults(int id)
        {
            TestMgtVM response = new TestMgtVM();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetTestResults}?id={id}");
                response = JsonConvert.DeserializeObject<TestMgtVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AddTest(TestVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{TeacherRoutes.AddTest}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AddChat(ChatVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{TeacherRoutes.AddChat}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AddChatRoom(ChatInfoVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{TeacherRoutes.AddChatRoom}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> AddTestResult(TestVM model)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.PostAsync($"{TeacherRoutes.AddTestResult}", model);
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<TestVM> GetTestById(int id)
        {
            TestVM response = new TestVM();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetTestById}?id={id}");
                response = JsonConvert.DeserializeObject<TestVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<Response> DeleteTestById(int id)
        {
            Response response = new Response();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.DeleteTestById}?id={id}");
                response = JsonConvert.DeserializeObject<Response>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<TestMgtVM> GetStudentList(int id)
        {
            TestMgtVM response = new TestMgtVM();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetStudentList}?id={id}");
                response = JsonConvert.DeserializeObject<TestMgtVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<SubjectVM>> GetStudentSubjects(int id)
        {
            List<SubjectVM> response = new List<SubjectVM>();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetStudentSubjects}?id={id}");
                response = JsonConvert.DeserializeObject<List<SubjectVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<TestMgtVM> GetTestResultsBySubjectId(int subjectId, int studentId)
        {
            TestMgtVM response = new TestMgtVM();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetTestResultsBySubjectId}?subjectId={subjectId}&studentId={studentId}");
                response = JsonConvert.DeserializeObject<TestMgtVM>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<UserVM>> GetChatUserList(int id)
        {
            List<UserVM> response = new List<UserVM>();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetChatUserList}?id={id}");
                response = JsonConvert.DeserializeObject<List<UserVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<UserVM>> GetChatPeople(int id)
        {
            List<UserVM> response = new List<UserVM>();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetChatPeople}?id={id}");
                response = JsonConvert.DeserializeObject<List<UserVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }


        public async Task<List<ChatVM>> GetChatRoomHistory(int id, long userId)
        {
            List<ChatVM> response = new List<ChatVM>();
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetChatRoomHistory}?id={id}&userId={userId}");
                response = JsonConvert.DeserializeObject<List<ChatVM>>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }



        public async Task LoginForChat(string username, string password)
        {
            try
            {
                string json = await httpClient.GetAsync($"http://localhost/ChatApp/Account/LoginChat?username={username}&password={password}");
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<int> GetNewChatCount(int id)
        {
            int response = 0;
            try
            {
                string json = await httpClient.GetAsync($"{TeacherRoutes.GetNewChatCount}?id={id}");
                response = JsonConvert.DeserializeObject<int>(json);
            }
            catch (Exception ex)
            {

            }
            return response;
        }
    }
}