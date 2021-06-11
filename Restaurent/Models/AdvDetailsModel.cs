using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class AdvDetailsModel
    {

        public AdvDetailsModel()
        {
            ImageUrls = new List<string>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public List<string> ImageUrls { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public DateTime PostedOn { get; set; }

        public DateTime StartsOn { get; set; }

        public DateTime EndsOn { get; set; }

        public string PostedBy { get; set; }

        public string Location { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public string Status { get; set; }
        public bool IsFeatured { get; set; }

    }
}