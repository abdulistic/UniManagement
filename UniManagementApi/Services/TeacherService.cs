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
        Task<TestMgtVM> GetTestResults(int id);
        Task<TestMgtVM> GetStudentList(int id);
        Task<Response> AddTestResult(TestVM model);
        Task<List<SubjectVM>> GetStudentSubjects(int id);
        Task<TestMgtVM> GetTestResultsBySubjectId(int subjectId, int studentId);
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


                        uniTest.TestName = model.TestName;
                        uniTest.CreatedOn = model.CreatedOn;
                        uniTest.TotalMarks = model.TotalMarks;

                        context.UniTests.Update(uniTest);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        UniTest uniClass = await context.UniTests?.FirstOrDefaultAsync(x => x.TestName.ToLower() == model.TestName.ToLower());

                        if (uniClass == null)
                        {
                            UniTest registration = new UniTest()
                            {
                                TestName = model.TestName,
                                SubjectId = model.SubjectId,
                                CreatedOn = model.CreatedOn,
                                IsActive = true,
                                TotalMarks = model.TotalMarks
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
                        TotalMarks = x.TotalMarks

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


        public async Task<TestMgtVM> GetTestResults(int id)
        {
            TestMgtVM subjectMgt = new TestMgtVM();

            try
            {
                UniTest testList = context.UniTests.FirstOrDefault(x => x.IsActive && x.TestId == id);
                Subject subject = context.Subjects.FirstOrDefault(x => x.IsActive && x.SubjectId == testList.SubjectId);
                UniClass testClass = context.UniClasses.FirstOrDefault(x => x.IsActive ?? false && x.ClassId == subject.ClassId);
                List<AssignClassStudent> classStudents = await context.AssignClassStudents.Where(x => x.ClassId == testClass.ClassId).ToListAsync();

                List<long> studentIds = classStudents.Select(x => x.StudentId).ToList();

                List<User> userList = await context.Users.Where(x => x.IsActive ?? false).ToListAsync();

                List<User> testUsers = userList.Where(x => studentIds.Contains(x.UserId)).ToList();

                List<TestResult> resultList = await context.TestResults.Where(x => x.IsActive && x.TestId == id).ToListAsync();

                string marks;

                if (userList?.Count > 0)
                {
                    subjectMgt.TestList = testUsers.Select(x => new TestVM()
                    {
                        SubjectId = (int)x.UserId,
                        SubjectName = $"{x.FirstName} {x.LastName}",
                        TotalMarks = !string.IsNullOrEmpty(marks = resultList?.FirstOrDefault(s => s.StudentId == x.UserId)?.StudentMarks)
                        ? Convert.ToInt32(marks) : 0,
                        TestResultId = resultList?.FirstOrDefault(s => s.StudentId == x.UserId)?.ResultId ?? 0

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return subjectMgt;
        }


        public async Task<Response> AddTestResult(TestVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {

                    if (model?.TestResultId > 0)
                    {
                        TestResult uniTest = await context.TestResults?.FirstOrDefaultAsync(x => x.ResultId == model.TestResultId);


                        uniTest.StudentMarks = model.TotalMarks.ToString();

                        context.TestResults.Update(uniTest);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        TestResult uniClass = await context.TestResults?.FirstOrDefaultAsync(x => x.TestId == model.TestId && x.StudentId == model.SubjectId);

                        if (uniClass == null)
                        {
                            TestResult registration = new TestResult()
                            {
                                StudentId = model.SubjectId,
                                TestId = model.TestId,
                                CreatedOn = DateTime.Now,
                                IsActive = true,
                                StudentMarks = model.TotalMarks.ToString()
                            };


                            context.TestResults.Add(registration);
                            await context.SaveChangesAsync();

                            if (registration.ResultId > 0)
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

        public async Task<TestMgtVM> GetStudentList(int id)
        {
            TestMgtVM subjectMgt = new TestMgtVM();

            try
            {
                List<Subject> subject = await context.Subjects.Where(x => x.IsActive).Where(s => s.TeacherId == id).ToListAsync();

                List<int?> classIds = subject.Select(x => x.ClassId).ToList();

                List<AssignClassStudent> classStudents = await context.AssignClassStudents.Where(x => classIds.Contains(x.ClassId)).ToListAsync();

                List<long> studentIds = classStudents.Select(x => x.StudentId).ToList();

                List<User> userList = await context.Users.Where(x => x.IsActive ?? false).Where(x => studentIds.Contains(x.UserId)).ToListAsync();

                List<TestResult> resultList = await context.TestResults.Where(x => x.IsActive).Where(s => s.TestId == id).ToListAsync();

                List<UniTest> uniTests = await context.UniTests.Where(x => x.IsActive).
                    Where(s => resultList.Select(o => o.TestId).ToList().Contains(s.TestId)).ToListAsync();

                if (userList?.Count > 0)
                {
                    subjectMgt.TestList = userList.Select(x => new TestVM()
                    {
                        SubjectId = (int)x.UserId,
                        SubjectName = $"{x.FirstName} {x.LastName}",
                        TotalMarks = CalculateGrades(resultList.Where(s => s.StudentId == x.UserId).ToList(), uniTests),

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return subjectMgt;
        }

        int CalculateGrades(List<TestResult> resultList, List<UniTest> uniTests, int SubjectId = 0)
        {
            int avgGrade = 0;

            try
            {
                if (SubjectId > 0)
                {
                    int totalMarks = uniTests.Where(s => s.SubjectId == SubjectId).Select(x => x.TotalMarks).Sum();
                    int totalMarksGaine = resultList.
                        Where(a => uniTests.Where(s => s.SubjectId == SubjectId).Select(x => x.TestId).Contains(a.TestId))
                        .Select(x => Convert.ToInt32(x?.StudentMarks ?? "0")).Sum();

                    double percentage = (double)totalMarksGaine / (double)totalMarks;

                    avgGrade = Convert.ToInt32(percentage * 100);
                }
                else
                {
                    int totalMarks = uniTests.Select(x => x.TotalMarks).Sum();
                    int totalMarksGaine = resultList.Select(x => Convert.ToInt32(x?.StudentMarks ?? "0")).Sum();

                    double percentage = (double)totalMarksGaine / (double)totalMarks;

                    avgGrade = Convert.ToInt32(percentage * 100);
                }

            }
            catch (Exception ex)
            {
            }

            return avgGrade;
        }

        public async Task<List<SubjectVM>> GetStudentSubjects(int id)
        {
            List<SubjectVM> subjectMgt = new List<SubjectVM>();

            try
            {
                AssignClassStudent studentClass = context.AssignClassStudents.FirstOrDefault(x => x.StudentId == id);
                List<Subject> subject = await context.Subjects.Where(x => x.IsActive).Where(s => s.ClassId == studentClass.ClassId).ToListAsync();

                List<TestResult> resultList = await context.TestResults.Where(x => x.IsActive).Where(s => s.StudentId == id).ToListAsync();

                List<UniTest> uniTests = await context.UniTests.Where(x => x.IsActive).
                    Where(s => resultList.Select(o => o.TestId).ToList().Contains(s.TestId)).ToListAsync();

                if (subject?.Count > 0)
                {
                    subjectMgt = subject.Select(x => new SubjectVM()
                    {
                        SubjectId = (int)x.SubjectId,
                        SubjectName = x.SubjectName,
                        Grades = CalculateGrades(resultList.Where(s => s.StudentId == id).ToList(), uniTests, (int)x.SubjectId),

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return subjectMgt;
        }


        public async Task<TestMgtVM> GetTestResultsBySubjectId(int subjectId, int studentId)
        {
            TestMgtVM subjectMgt = new TestMgtVM();

            try
            {
                List<UniTest> testList = await context.UniTests.Where(x => x.IsActive).Where(s => s.TestId == subjectId).ToListAsync();

                List<TestResult> resultList = await context.TestResults.Where(x => x.StudentId == studentId).ToListAsync();

                TestResult marks;

                if (testList?.Count > 0)
                {
                    subjectMgt.TestList = testList.Select(x => new TestVM()
                    {
                        TestId = (int)x.TestId,
                        TestName = x.TestName,
                        TotalMarks = x.TotalMarks,
                        TestScore = (marks = resultList.FirstOrDefault(r => r.TestId == x.TestId)) != null &&
                        !string.IsNullOrEmpty(marks.StudentMarks)
                        ? Convert.ToInt32(marks.StudentMarks) : 0,
                        CreatedOn = marks?.CreatedOn ?? DateTime.Now

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return subjectMgt;
        }
    }
}
