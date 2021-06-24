using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(UserVM user, string token)
        {
            Id = (int)user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.UserName;
            Token = token;
        }

        public enum ErrorCode
        {
            Unauthorized = 701,
            SessionInvalid = 702,
            TokenExpired = 703,
            InvalidAudience = 704,
            Invalidalgorithm = 705,
        }

        public class CustomError
        {
            public string Error { get; }
            public ErrorCode Code { get; }

            public CustomError(string message, ErrorCode errorCode)
            {
                Error = message;
                Code = errorCode;
            }
        }

        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string Teacher = "Teacher";
            public const string Student = "Student";
        }
    }
}
