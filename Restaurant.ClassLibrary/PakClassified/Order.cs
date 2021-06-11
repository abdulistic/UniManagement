using Restaurant.ClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.PakClassified
{
   public class Order
    {
        public Order()
        {
            OrderDetail = new List<OrderDetail>();
        }
        public int Id { get; set; }
        public DateTime AddedOn { get; set; }
        public virtual User User { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }


        

    }
}
