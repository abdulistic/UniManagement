using DemoClassLibrary.UsersMgt;
using Restaurant.ClassLibrary.PakClassified;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.ClassLibrary.UsersMgt
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string LoginId { get; set; }
  
        //[Required]
        //[MaxLength(20)]
        //[Column(TypeName = "varchar")]
        //public string FirstName { get; set; }
        //[Required]
        //[MaxLength(20)]
        //[Column(TypeName = "varchar")]
        //public string LastName { get; set; }




        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string Password { get; set; }


        [Required]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string ConfirmPassword { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string ContactNumber { get; set; }

        public  string Address { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string Email { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string ImageUrl { get; set; }

        public Nullable<DateTime> BirthDate { get; set; }

        public Nullable<bool> IsActive { get; set; }


        [MaxLength(100)]
        [Column(TypeName = "varchar")]
        public string FullName { get; set; }

        //[MaxLength(255)]
        //[Column(TypeName = "varchar")]
        //public virtual UserSecurityQuestion SecurityQuestion { get; set; }

        //[MaxLength(255)]
        //[Column(TypeName = "varchar")]
        //public string SecurityAnswer { get; set; }
        public virtual Advertisement Advertisement { get; set; }

        [Required]
        public virtual Role Role { get; set; }
        //[Required]
        public virtual UserGender Gender { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<UserImage> Image { get; set; }

        public bool IsInRole(int id)
        {
            return Role != null && Role.Id == id;
        }

    }
}
