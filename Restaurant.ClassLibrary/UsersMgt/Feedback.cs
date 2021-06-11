using Restaurant.ClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.UsersMgt
{
    public class Feedback
    {
        public int Id { get; set; }
        public string UserFeedback { get; set; }
        public virtual User User { get; set; }
        public DateTime SubmittedOn { get; set; }
    }
}
