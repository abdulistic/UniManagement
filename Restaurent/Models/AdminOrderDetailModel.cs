using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class AdminOrderDetailModel
    {       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Extras { get; set; }
        public int Quantity { get; set; }
    }
}