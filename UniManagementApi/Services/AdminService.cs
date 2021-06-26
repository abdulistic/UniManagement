using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Restaurant.ClassLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniPortalModel;
using static UniManagementApi.SupportClasses.Utility;

namespace UniManagementApi.Services
{
    public interface IAdminService
    {
        Task<Response> AddUser(UserVM model);
        Task<Response> AddClass(ClassVM model);
        Task<Response> AddSubject(SubjectVM model);
        Task<Response> AssignClass(SubjectVM model);
        Task<List<UserVM>> GetUserList();
        Task<List<ClassVM>> GetClassList();
        Task<SubjectMgtVM> GetSubjectList();
        Task<SubjectMgtVM> GetStudentList();
        Task<UserVM> GetUserById(long userId);
        Task<ClassVM> GetClassById(long classId);
        Task<SubjectVM> GetSubjectById(long subjectId);
        Task<Response> DeleteUserById(long userId);
        Task<Response> DeleteClassById(long classId);
        Task<Response> DeleteSubjectById(long subjectId);
        Task<Response> DeassignClass(long userId);

        Task<UserVM> GetUser(UserVM model);
    }

    public class AdminService : IAdminService
    {
        private readonly UniPortalContext context;
        private readonly IConfiguration configuration;

        public AdminService(UniPortalContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<Response> AddUser(UserVM model)
        {
            Response response = new Response();
            try
            {
                //ValidationResult validationResult = new IdentityVmValidator().Validate(model);

                if (model != null)
                {

                    if (model?.UserId > 0)
                    {
                        User user = await context.Users?.FirstOrDefaultAsync(x => x.UserId == model.UserId);


                        user.FirstName = model.FirstName;
                        user.LastName = model.LastName;
                        user.Email = model.Email;
                        user.PhoneNumber = model.PhoneNumber;

                        context.Users.Update(user);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        User user = await context.Users?.FirstOrDefaultAsync(x => x.UserName.ToLower() == model.UserName.ToLower());

                        if (user == null)
                        {
                            string eStr = passwordEncrypt(model.Password, configuration.GetValue<string>("EncryptionKey"));

                            User registration = new User()
                            {
                                UserName = model.UserName,
                                Email = model.Email,
                                PhoneNumber = model.PhoneNumber,
                                FirstName = model.FirstName,
                                LastName = model.LastName,
                                Password = eStr + "",
                                RoleId = Convert.ToInt32(model.Role),
                                CreatedOn = DateTime.Now,
                                IsActive = true,
                                RegId = Guid.NewGuid().ToString()
                            };


                            context.Users.Add(registration);
                            await context.SaveChangesAsync();

                            if (registration.UserId > 0)
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

        public async Task<Response> AddClass(ClassVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {

                    if (model?.ClassId > 0)
                    {
                        UniClass uniClass = await context.UniClasses?.FirstOrDefaultAsync(x => x.ClassId == model.ClassId);


                        uniClass.ClassName = model.ClassName;

                        context.UniClasses.Update(uniClass);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        UniClass uniClass = await context.UniClasses?.FirstOrDefaultAsync(x => x.ClassName.ToLower() == model.ClassName.ToLower());

                        if (uniClass == null)
                        {
                            UniClass registration = new UniClass()
                            {
                                ClassName = model.ClassName,
                                CreatedOn = DateTime.Now,
                                IsActive = true
                            };


                            context.UniClasses.Add(registration);
                            await context.SaveChangesAsync();

                            if (registration.ClassId > 0)
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

        public async Task<Response> AddSubject(SubjectVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {

                    if (model?.SubjectId > 0)
                    {
                        Subject uniClass = await context.Subjects?.FirstOrDefaultAsync(x => x.SubjectId == model.SubjectId);


                        uniClass.SubjectName = model.SubjectName;
                        uniClass.TeacherId = model.TeacherId;
                        uniClass.ClassId = model.ClassId;

                        context.Subjects.Update(uniClass);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        Subject uniClass = await context.Subjects?.FirstOrDefaultAsync(x => x.SubjectName.ToLower() == model.SubjectName.ToLower());

                        if (uniClass == null)
                        {
                            Subject registration = new Subject()
                            {
                                SubjectName = model.SubjectName,
                                TeacherId = model.TeacherId,
                                CreatedOn = DateTime.Now,
                                IsActive = true,
                                ClassId = model.ClassId
                            };


                            context.Subjects.Add(registration);
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

        public async Task<Response> AssignClass(SubjectVM model)
        {
            Response response = new Response();
            try
            {
                if (model != null)
                {
                    AssignClassStudent uniClass = await context.AssignClassStudents?.FirstOrDefaultAsync(x => x.StudentId == model.SubjectId);

                    if (uniClass != null)
                    {


                        uniClass.ClassId = model.ClassId;

                        context.AssignClassStudents.Update(uniClass);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
                    }
                    else
                    {
                        AssignClassStudent registration = new AssignClassStudent()
                        {
                            StudentId = model.SubjectId,
                            CreatedOn = DateTime.Now,
                            ClassId = model.ClassId
                        };


                        context.AssignClassStudents.Add(registration);
                        await context.SaveChangesAsync();

                        if (registration.AssignClassId > 0)
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

        public async Task<List<ClassVM>> GetClassList()
        {
            List<ClassVM> classesVM = new List<ClassVM>();

            try
            {
                List<UniClass> classList = await context.UniClasses.Where(x => x.IsActive ?? false).ToListAsync();

                if (classList?.Count > 0)
                {
                    classesVM = classList.Select(x => new ClassVM()
                    {
                        ClassId = x.ClassId,
                        ClassName = x.ClassName,
                        CreatedOn = x.CreatedOn

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return classesVM;
        }

        public async Task<List<UserVM>> GetUserList()
        {
            List<UserVM> usersVM = new List<UserVM>();

            try
            {
                List<User> userList = await context.Users.Where(x => x.IsActive ?? false).ToListAsync();

                if (userList?.Count > 0)
                {
                    usersVM = userList.Select(x => new UserVM()
                    {
                        UserId = x.UserId,
                        UserName = x.UserName,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        Role = Enum.GetName(typeof(RoleEnum), x.RoleId),
                        CreatedOn = x.CreatedOn,
                        RegId = x.RegId

                    }).ToList();
                }
            }
            catch (Exception ex)
            {

            }

            return usersVM;
        }

        public async Task<SubjectMgtVM> GetSubjectList()
        {
            SubjectMgtVM subjectMgt = new SubjectMgtVM();

            try
            {
                List<Subject> subjectList = await context.Subjects.Where(x => x.IsActive).ToListAsync();
                List<UniClass> classList = await context.UniClasses.Where(x => x.IsActive ?? false).ToListAsync();
                List<User> userList = await context.Users.Where(x => x.IsActive ?? false).ToListAsync();

                if (subjectList?.Count > 0)
                {
                    subjectMgt.SubjectList = subjectList.Select(x => new SubjectVM()
                    {
                        SubjectId = x.SubjectId,
                        SubjectName = x.SubjectName,
                        TeacherName = $"{ userList?.FirstOrDefault(s => s.UserId == x.TeacherId)?.FirstName ?? "N/A"} {userList?.FirstOrDefault(s => s.UserId == x.TeacherId)?.LastName}",
                        CreatedOn = x.CreatedOn,
                        ClassName = classList?.FirstOrDefault(s => s.ClassId == x.ClassId)?.ClassName ?? "N/A"

                    }).ToList();
                }


                subjectMgt.TeachersList = userList.Where(w => w.RoleId == (int)RoleEnum.Teacher).Select(x => new SelectListItem()
                {
                    Value = x.UserId.ToString(),
                    Text = $"{x.FirstName} {x.LastName}"

                }).ToList();



                subjectMgt.ClassList = classList.Select(x => new SelectListItem()
                {
                    Value = x.ClassId.ToString(),
                    Text = x.ClassName

                }).ToList();

            }
            catch (Exception ex)
            {

            }

            return subjectMgt;
        }



        public async Task<UserVM> GetUserById(long userId)
        {
            UserVM userVM = new UserVM();
            try
            {
                User user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                if (user != null)
                {
                    userVM = new UserVM()
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Role = Enum.GetName(typeof(RoleEnum), user.RoleId),
                        CreatedOn = user.CreatedOn

                    };
                }
            }
            catch (Exception ex)
            {

            }

            return userVM;
        }

        public async Task<UserVM> GetUser(UserVM model)
        {
            UserVM userVM = new UserVM();
            try
            {

                User user = await context.Users
                    .FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(model.UserName.ToLower()));

                if (user != null)
                {
                    string dStr = passwordDecrypt(user.Password, configuration.GetValue<string>("EncryptionKey"));

                    if (dStr.Equals(model.Password))
                    {
                        userVM = new UserVM()
                        {
                            UserId = user.UserId,
                            UserName = user.UserName,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            RegId = user.RegId,
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            Role = Enum.GetName(typeof(RoleEnum), user.RoleId),
                            CreatedOn = user.CreatedOn
                        };

                        AuthenticateResponse authenticate = Authenticate(userVM);

                        userVM.Token = authenticate.Token;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return userVM;
        }

        public async Task<ClassVM> GetClassById(long classId)
        {
            ClassVM response = new ClassVM();
            try
            {
                UniClass myClass = await context.UniClasses.FirstOrDefaultAsync(x => x.ClassId == classId);

                if (myClass != null)
                {
                    response = new ClassVM()
                    {
                        ClassId = myClass.ClassId,
                        ClassName = myClass.ClassName,
                        CreatedOn = myClass.CreatedOn,
                    };
                }
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
                Subject subject = await context.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);

                if (subject != null)
                {
                    response = new SubjectVM()
                    {
                        SubjectId = subject.SubjectId,
                        SubjectName = subject.SubjectName,
                        TeacherId = subject?.TeacherId ?? 0,
                        ClassId = subject?.ClassId ?? 0,
                        CreatedOn = subject.CreatedOn,
                    };
                }
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
                if (userId > 0)
                {
                    User user = await context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

                    if (user != null)
                    {
                        user.IsActive = false;

                        context.Users.Update(user);
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

        public async Task<Response> DeassignClass(long userId)
        {
            Response response = new Response();
            try
            {
                if (userId > 0)
                {
                    AssignClassStudent uniClass = await context.AssignClassStudents?.FirstOrDefaultAsync(x => x.StudentId == userId);

                    if (uniClass != null)
                    {
                        uniClass.ClassId = 0;

                        context.AssignClassStudents.Update(uniClass);
                        await context.SaveChangesAsync();

                        response.Status = ResponseStatus.OK;
                        response.Message = "Updated successfully.";
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

        public async Task<Response> DeleteClassById(long classId)
        {
            Response response = new Response();
            try
            {
                if (classId > 0)
                {
                    UniClass myClass = await context.UniClasses.FirstOrDefaultAsync(x => x.ClassId == classId);

                    if (myClass != null)
                    {
                        myClass.IsActive = false;

                        context.UniClasses.Update(myClass);
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

        public async Task<Response> DeleteSubjectById(long subjectId)
        {
            Response response = new Response();
            try
            {
                if (subjectId > 0)
                {
                    Subject subject = await context.Subjects.FirstOrDefaultAsync(x => x.SubjectId == subjectId);

                    if (subject != null)
                    {
                        subject.IsActive = false;

                        context.Subjects.Update(subject);
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


        public async Task<SubjectMgtVM> GetStudentList()
        {
            SubjectMgtVM subjectMgt = new SubjectMgtVM();

            try
            {
                List<UniClass> classList = await context.UniClasses.Where(x => x.IsActive ?? false).ToListAsync();
                List<AssignClassStudent> classStudents = await context.AssignClassStudents.ToListAsync();

                List<User> allUsers = await context.Users.Where(x => x.IsActive ?? false).ToListAsync();

                List<User> userList = allUsers.Where(x => x.RoleId == (int)RoleEnum.Student).ToList();

                int classId = 0;
                string className;

                if (userList?.Count > 0)
                {
                    subjectMgt.SubjectList = userList.Select(x => new SubjectVM()
                    {
                        SubjectId = (int)x.UserId,
                        SubjectName = $"{x.FirstName} {x.LastName}",
                        CreatedOn = x.CreatedOn,
                        ClassId = classId = classStudents?.FirstOrDefault(s => s.StudentId == x.UserId)?.ClassId ?? 0,
                        ClassName = (classId > 0) ? classList?.FirstOrDefault(s => s.ClassId == classId)?.ClassName : "N/A",

                    }).ToList();
                }



                subjectMgt.ClassList = classList.Select(x => new SelectListItem()
                {
                    Value = x.ClassId.ToString(),
                    Text = x.ClassName

                }).ToList();

            }
            catch (Exception ex)
            {

            }

            return subjectMgt;
        }



        #region utils

        public string passwordEncrypt(string inText, string key)
        {
            byte[] bytesBuff = Encoding.Unicode.GetBytes(inText);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    inText = Convert.ToBase64String(mStream.ToArray());
                }
            }
            return inText;
        }
        //Decrypting a string
        public string passwordDecrypt(string cryptTxt, string key)
        {
            cryptTxt = cryptTxt.Replace(" ", "+");
            byte[] bytesBuff = Convert.FromBase64String(cryptTxt);
            using (Aes aes = Aes.Create())
            {
                Rfc2898DeriveBytes crypto = new Rfc2898DeriveBytes(key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                aes.Key = crypto.GetBytes(32);
                aes.IV = crypto.GetBytes(16);
                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cStream.Write(bytesBuff, 0, bytesBuff.Length);
                        cStream.Close();
                    }
                    cryptTxt = Encoding.Unicode.GetString(mStream.ToArray());
                }
            }
            return cryptTxt;
        }


        public AuthenticateResponse Authenticate(UserVM model)
        {
            // return null if user not found
            if (model == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(model);

            return new AuthenticateResponse(model, token);
        }


        private string generateJwtToken(UserVM user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("EncryptionKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.UserId.ToString()), new Claim("Role", user.Role) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
