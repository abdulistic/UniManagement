using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class LoginModel
    {
        private string userName;
        [Required]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "invalid email")]

        [Display(Name = "User Name")]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;

        [Required]
        [DataType(DataType.Password)]

        [Display(Name = "My Password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private bool rememberMe;

        [Display(Name = "Remeber Me")]
        public bool RememberMe
        {
            get { return rememberMe; }
            set { rememberMe = value; }
        }
    }
}