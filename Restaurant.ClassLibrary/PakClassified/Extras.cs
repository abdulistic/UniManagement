using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurant.ClassLibrary.PakClassified
{
    public class Extras : IListable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public virtual Category Categories { get; set; }
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
