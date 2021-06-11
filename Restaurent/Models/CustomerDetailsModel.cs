using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class CustomerDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string DOB { get; set; }

        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

    }
}