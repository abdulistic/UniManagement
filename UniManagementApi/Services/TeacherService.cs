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
        Task<List<UserVM>> GetChatUserList(int id);
        Task<Response> AddChat(ChatVM model);
        Task<Response> AddChatRoom(ChatInfoVM model);
        Task<List<UserVM>> GetChatPeople(int id);
        Task<List<ChatVM>> GetChatRoomHistory(int id);
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

        public async Task<Response> AddChatRoom(ChatInfoVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {
                    User sender = context.Users?.FirstOrDefault(x => x.UserId == model.SenderId);
                    User reciever = context.Users?.FirstOrDefault(x => x.UserId == model.RecieverId);

                    List<User> chatUsers = new List<User>() { sender, reciever };

                    string chatRoomName = Xor(new Guid(sender.RegId), new Guid(reciever.RegId)).ToString();

                    ChatRoom chatRoom = await context.ChatRooms?.FirstOrDefaultAsync(x => x.ChatRoomName.Equals(chatRoomName));

                    if (chatRoom == null)
                    {
                        ChatRoom registration = new ChatRoom()
                        {
                            ChatRoomName = chatRoomName,
                            CreatedOn = DateTime.Now
                        };


                        context.ChatRooms.Add(registration);
                        await context.SaveChangesAsync();

                        if (registration.ChatRoomId > 0)
                        {
                            List<ChatRoomMember> chatMember = chatUsers.Select(x => new ChatRoomMember
                            {
                                ChatRoomId = registration.ChatRoomId,
                                MemberUserId = x.UserId,
                                CreatedOn = DateTime.Now,

                            }).ToList();


                            context.ChatRoomMembers.AddRange(chatMember);
                            await context.SaveChangesAsync();

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

        public async Task<Response> AddChat(ChatVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {
                    Chat registration = new Chat()
                    {
                        ChatRoomId = model.ChatRoomId,
                        Message = model.Message,
                        RecieverId = (int)model.RecieverId,
                        SenderId = (int)model.SenderId,
                        CreatedOn = DateTime.Now,
                    };


                    context.Chats.Add(registration);
                    await context.SaveChangesAsync();

                    if (registration.ChatId > 0)
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

                List<TestResult> resultList = await context.TestResults.Where(x => x.IsActive)
                    .Where(s => userList.Select(a=>a.UserId).ToList().Contains(s.TestId)).ToListAsync();

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

        public async Task<List<UserVM>> GetChatUserList(int id)
        {
            List<UserVM> subjectMgt = new List<UserVM>();
            List<User> userList = new List<User>();
            try
            {
                User user = await context.Users.FirstAsync(x => x.UserId == id);

                if (user.RoleId == (int)RoleEnum.Student)
                {
                    AssignClassStudent studentClass = context.AssignClassStudents.FirstOrDefault(x => x.StudentId == id);
                    List<AssignClassStudent> classStudents = await context.AssignClassStudents.Where(x => x.StudentId != id).Where(x => x.ClassId == studentClass.ClassId).ToListAsync();
                    List<Subject> subjects = await context.Subjects.Where(x => x.IsActive).Where(s => s.ClassId == studentClass.ClassId).ToListAsync();

                    userList = await context.Users.Where(x => x.IsActive ?? false).
                        Where(s => classStudents.Select(a => a.StudentId).ToList().Contains(s.UserId)).ToListAsync();

                    List<User> teacherList = await context.Users.Where(x => x.IsActive ?? false).
                        Where(s => subjects.Select(a => a.TeacherId).ToList().Contains(s.UserId)).ToListAsync();

                    userList.AddRange(teacherList);
                }
                else if (user.RoleId == (int)RoleEnum.Teacher)
                {
                    List<Subject> subjects = await context.Subjects.Where(x => x.IsActive).Where(s => s.TeacherId == user.UserId).ToListAsync();

                    List<UniClass> classes = await context.UniClasses.Where(x => x.IsActive ?? false)
                        .Where(s => subjects.Select(c => c.ClassId).Contains(s.ClassId)).ToListAsync();

                    List<AssignClassStudent> classStudents = await context.AssignClassStudents
                        .Where(x => classes.Select(s => s.ClassId).ToList().Contains(x.ClassId ?? 0)).ToListAsync();

                    userList = await context.Users.Where(x => x.IsActive ?? false).
                        Where(s => classStudents.Select(a => a.StudentId).ToList().Contains(s.UserId)).ToListAsync();

                    List<User> teacherList = await context.Users.Where(x => x.IsActive ?? false).
                        Where(s => s.RoleId == (int)RoleEnum.Teacher).Where(x => x.UserId != id).ToListAsync();

                    List<User> admins = await context.Users.Where(x => x.IsActive ?? false).
                        Where(s => s.RoleId == (int)RoleEnum.Admin).ToListAsync();

                    userList.AddRange(teacherList);
                    userList.AddRange(admins);
                }

                List<ChatRoom> chatRooms = context.ChatRooms.ToList();

                foreach (User item in userList)
                {
                    bool exists = (chatRooms?.Any(s => s.ChatRoomName == Xor(new Guid(item.RegId), new Guid(user.RegId)).ToString())) ?? false;
                    if (!exists)
                    {
                        subjectMgt.Add(
                            new UserVM
                            {
                                FirstName = item.FirstName,
                                LastName = item.LastName,
                                RegId = item.RegId,
                                UserId = item.UserId

                            });
                    }
                        
                }

                //subjectMgt = userList
                //    .Where(x => !(chatRooms?.Count > 0) && (chatRooms.Any(s => s.ChatRoomName == Xor(new Guid(x.RegId), new Guid(user.RegId)).ToString())))
                //    .Select(s => new UserVM
                //    {
                //        FirstName = s.FirstName,
                //        LastName = s.LastName,
                //        RegId = s.RegId,
                //        UserId = s.UserId

                //    }).ToList();
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


        public async Task<List<UserVM>> GetChatPeople(int id)
        {
            List<UserVM> userList = new List<UserVM>();

            try
            {
                List<ChatRoomMember> myChats = await context.ChatRoomMembers.Where(x => x.MemberUserId == id).ToListAsync();
                List<ChatRoomMember> chatRecipient = await context.ChatRoomMembers.Where(u => u.MemberUserId != id)
                    .Where(x => myChats.Select(s => s.ChatRoomId).ToList().Contains(x.ChatRoomId)).ToListAsync();

                List<User> users = await context.Users.Where(x => chatRecipient.Select(x => x.MemberUserId).ToList().Contains(x.UserId)).ToListAsync();

                if (users?.Count > 0)
                {
                    userList = users.Select(x => new UserVM()
                    {
                        UserId = (int)x.UserId,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        CreatedOn = x.CreatedOn,
                        ChatRoomId = chatRecipient?.FirstOrDefault(s => s.MemberUserId == x.UserId)?.ChatRoomId ?? 0

                    }).OrderByDescending(o => o.CreatedOn).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return userList;
        }

        public async Task<List<ChatVM>> GetChatRoomHistory(int id)
        {
            List<ChatVM> chatList = new List<ChatVM>();

            try
            {
                List<Chat> myChats = await context.Chats.Where(x => x.ChatRoomId == id).ToListAsync();


                if (myChats?.Count > 0)
                {
                    chatList = myChats.Select(x => new ChatVM()
                    {
                        ChatId = (int)x.ChatId,
                        Message = x.Message,
                        SenderId = x.SenderId,
                        CreatedOn = x.CreatedOn

                    }).OrderBy(o => o.CreatedOn).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return chatList;
        }



        public Guid Xor(Guid g1, Guid g2)
        {
            var ba1 = g1.ToByteArray();
            var ba2 = g2.ToByteArray();
            var ba3 = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                ba3[i] = (byte)(ba1[i] ^ ba2[i]);
            }


            //bool exists = chatRooms?.Count > 0 ? chatRooms.Any(s => s.ChatRoomName == (new Guid(ba3)).ToString()) : false;

            return new Guid(ba3);
        }
    }
}
