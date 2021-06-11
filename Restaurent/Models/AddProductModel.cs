using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurent.Models
{
    public class AddProductModel
    {

            [Required]
            public int Id { get; set; }

        [Required(ErrorMessage = "Title field is required")]
        [MaxLength(200)] 
        [Column(TypeName = "varchar")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description field is required")]
        [MaxLength(1000)]
        [Column(TypeName = "varchar")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price field is required")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Calories field is required")]
        public float Calories { get; set; }

        [Required(ErrorMessage = "Fat field is required")]
        public float Fat { get; set; }

        [Required(ErrorMessage = "SaturatedFat field is required")]
        public float SaturatedFat { get; set; }

        [Required(ErrorMessage = "TransFat field is required")]
        public float TransFat { get; set; }

        [Required(ErrorMessage = "Cholestrol field is required")]
        public float Cholestrol { get; set; }


        [Required(ErrorMessage = "Sodium field is required")]
        public float Sodium { get; set; }

        [Required(ErrorMessage = "Carbohydrates field is required")]
        public float Carbohydrates { get; set; }

        [Required(ErrorMessage = "Fiber field is required")]
        public float Fiber { get; set; }

        [Required(ErrorMessage = "Sugar field is required")]
        public float Sugar { get; set; }

        [Required(ErrorMessage = "Protein field is required")]
        public float Protein { get; set; }


        [Required(ErrorMessage = "Category field is required")]
        public int Category { get; set; }

           
        [Required(ErrorMessage = "Status field is required")]
        public int Status { get; set; }


            public string Image { get; set; }

            public bool IsFeatured { get; set; }

    }
}