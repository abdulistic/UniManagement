using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class ProductDetailsModel
    {
        public ProductDetailsModel()
        {
            DetailExtrasModel = new List<DetailExtrasModel>();
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public AdvStatusModel Status { get; set; }

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

        public virtual ICollection<DetailExtrasModel> DetailExtrasModel { get; set; }

    }
}