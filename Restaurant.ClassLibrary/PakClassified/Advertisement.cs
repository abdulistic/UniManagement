using Restaurant.ClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.PakClassified
{
    public class Advertisement 
    {

        public Advertisement()
        {
            Images = new List<AdvertisementImage>();
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(300)]
        public string Title { get; set; }

        [Column(TypeName = "varchar")]
        public string Description { get; set; }

        //[Required]
        public float Price { get; set; }

        public float Calories { get; set; }
        public float Fat { get; set; }

        public float SaturatedFat { get; set; }
        public float TransFat { get; set; }
        public float Cholestrol { get; set; }
        public float Sodium { get; set; }
        public float Carbohydrates { get; set; }
        public float Fiber { get; set; }
        public float Sugar { get; set; }
        public float Protein { get; set; }



        public DateTime AddedOn { get; set; }


        //[Required]
        public virtual Category Category { get; set; }
        public int? Status_Id { get; set; }
        [ForeignKey("Status_Id")]
        public virtual AdvertisementStatus Status { get; set; }
        public virtual ICollection<AdvertisementImage> Images { get; set; }
        public bool IsFeatured { get; set; }


       


    }
}
