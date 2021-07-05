using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class ClassVM
    {
        public int ClassId { get; set; }
        private string className;

        [Required]
        [Display(Name = "Class Name")]
        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }

        public DateTime CreatedOn { get; set; }

    }
}
