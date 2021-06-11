using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class OrderModel
    {
        public OrderModel()
        {
            OrderItem = new List<OrderDetailModel>();
        }
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string UserName { get; set; }
        public DateTime dateTime { get; set; }

        public float Price { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderDetailModel> OrderItem { get; set; }

    }
}