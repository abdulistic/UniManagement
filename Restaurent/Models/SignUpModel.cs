using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurent.Models
{
    public class SignUpModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string LoginId { get; set; }


        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        
        public string Password { get; set; }


       
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string ContactNumber { get; set; }


        [Required(ErrorMessage = "This field is required")]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Address { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Check email address format!")]
        public string Email { get; set; }

        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string ImageUrl { get; set; }

        public string BirthDate { get; set; }

        public bool Agree { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [MaxLength(20)]
        [Column(TypeName = "varchar")]
        public string FullName { get; set; }

        public int Gender { get; set; }

        public int City { get; set; }
        //[MaxLength(255)]
        //[Column(TypeName = "varchar")]
        //public string SecurityAnswer { get; set; }
        //public virtual Advertisement Advertisement { get; set; }

        //[Required]
        //public string Role { get; set; }
        ////[Required]
        //public virtual UserGender Gender { get; set; }
        //public virtual ICollection<UserImage> Image { get; set; }

        //public bool IsInRole(int id)
        //{
        //    return Role != null && Role.Id == id;
        //}

    }

}
