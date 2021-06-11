using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class FeedbackModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string Conatact { get; set; }
        public string Message { get; set; }
    }
}