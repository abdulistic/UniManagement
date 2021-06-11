using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurent.ViewModels
{
    public class UserVM
    {
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

    }
}