using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class User
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int RoleId { get; set; }
        public bool? IsActive { get; set; }
    }
}
