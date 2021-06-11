using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class ChefDetailExtrasModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
    }
}