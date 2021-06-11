using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class SubjectVM
    {
        public int SubjectId { get; set; }

        private string subjectName;

        [Required]
        [Display(Name = "Subject Name")]
        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }
        public long TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
