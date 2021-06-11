using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class ChefOrderModel
    {
        public ChefOrderModel()
        {
            ChefOrderItem = new List<ChefOrderDetailModel>();
            //ChefDetailExtras = new List<ChefDetailExtrasModel>();
        }
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }

        public float Price { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public virtual ICollection<ChefOrderDetailModel> ChefOrderItem { get; set; }
        //public virtual ICollection<ChefDetailExtrasModel> ChefDetailExtras { get; set; }
    }
}