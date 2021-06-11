using System;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class UserVM
    {
        private long userId;

        public long UserId
        {
            get { return userId; }
            set { userId = value; }
        }


        private string userName;
        [Required]

        [Display(Name = "User Name")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;

        [Required]
        [DataType(DataType.Password)]

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        private string firstName;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string email;

        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string phoneNumber;

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private string role;

        [Required]
        public string Role
        {
            get { return role; }
            set { role = value; }
        }


        public DateTime CreatedOn { get; set; }


    }
}
