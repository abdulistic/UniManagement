using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.ClassLibrary.PakClassified
{
    public class NutritionInfo
    {
        public int Calories { get; set; }
        public int Fat { get; set; }

        public int SaturatedFat { get; set; }
        public int TransFat { get; set; }
        public int Cholestrol { get; set; }
        public int Sodium { get; set; }
        public int Carbohydrates { get; set; }
        public int Fiber { get; set; }
        public int Sugar { get; set; }
        public int Protein { get; set; }


    }
}
