using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniPortalModel;
using static UniManagementApi.SupportClasses.Utility;

namespace UniManagementApi.Services
{
    public interface ITeacherService
    {
        Task<List<SubjectVM>> GetSubjectList(int id);
        Task<Response> AddTest(TestVM model);
        Task<List<TestVM>> GetTestList(int id);
        Task<Response> DeleteTestById(int id);
        Task<TestVM> GetTestById(long id);
    }

    public class TeacherService : ITeacherService
    {
        private readonly UniPortalContext context;
        private readonly IConfiguration configuration;

        public TeacherService(UniPortalContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<Response> AddTest(TestVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {

                    if (model?.TestId > 0)
                    {
                        UniTest uniTest = await context.UniTests?.FirstOrDefaultAsync(x => x.TestId == model.TestId);


                        uniTest.TestName = model.SubjectName;
                        uniTest.CreatedOn = model.CreatedOn;

                        context.UniTests.Update(uniTest);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        Subject uniClass = await context.Subjects?.FirstOrDefaultAsync(x => x.SubjectName.ToLower() == model.SubjectName.ToLower());

                        if (uniClass == null)
                        {
                            UniTest registration = new UniTest()
                            {
                                TestName = model.TestName,
                                SubjectId = model.SubjectId,
                                CreatedOn = DateTime.Now,
                                IsActive = true,
                            };


                            context.UniTests.Add(registration);
                            await context.SaveChangesAsync();

                            if (registration.SubjectId > 0)
                            {
                                response.Status = ResponseStatus.OK;
                                response.Message = "Registration successful.";
                            }
                            else
                            {
                                response.Status = ResponseStatus.Error;
                                response.Message = "Registration failed.";
                            }
                        }
                    }
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Registration failed.";
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response> DeleteTestById(int id)
        {
            Response response = new Response();
            try
            {
                if (id > 0)
                {
                    UniTest uniTest = await context.UniTests.FirstOrDefaultAsync(x => x.TestId == id);

                    if (uniTest != null)
                    {
                        uniTest.IsActive = false;

                        context.UniTests.Update(uniTest);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "User deleted successfully";
                    }
                    else
                    {
                        response.Status = ResponseStatus.Error;
                        response.Message = "Task Failed";
                    }
                }
                else
                {
                    response.Status = ResponseStatus.Error;
                    response.Message = "Task failed.";
                }
            }
            catch (Exception ex)
            {
                response.Status = ResponseStatus.Error;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<List<SubjectVM>> GetSubjectList(int id)
        {
            List<SubjectVM> response = new List<SubjectVM>();

            try
            {
                List<Subject> subjectList = await context.Subjects.Where(x => x.IsActive && x.TeacherId == id).ToListAsync();
                List<UniClass> classList = await context.UniClasses.Where(x => x.IsActive ?? false).ToListAsync();

                if (subjectList?.Count > 0)
                {
                    response = subjectList.Select(x => new SubjectVM()
                    {
                        SubjectId = x.SubjectId,
                        SubjectName = x.SubjectName,
                        CreatedOn = x.CreatedOn,
                        ClassName = classList.FirstOrDefault(s => s.ClassId == x.ClassId)?.ClassName

                    }).ToList();
                }
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
                List<UniTest> subjectList = await context.UniTests.Where(x => x.IsActive && x.SubjectId == id).ToListAsync();

                if (subjectList?.Count > 0)
                {
                    response = subjectList.Select(x => new TestVM()
                    {
                        TestId = x.TestId,
                        TestName = x.TestName,
                        CreatedOn = x.CreatedOn,

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }

        public async Task<TestVM> GetTestById(long id)
        {
            TestVM response = new TestVM();
            try
            {
                UniTest myClass = await context.UniTests.FirstOrDefaultAsync(x => x.TestId == id);

                if (myClass != null)
                {
                    response = new TestVM()
                    {
                        TestId = myClass.TestId,
                        TestName = myClass.TestName,
                        CreatedOn = myClass.CreatedOn,
                    };
                }
            }
            catch (Exception ex)
            {

            }

            return response;
        }
    }
}
