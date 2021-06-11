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
    public class OrderDetail :IListable
    {
        private OrderExtra orderExtra;

        public OrderDetail()
        {
            ProductOrderExtra = new List<OrderExtra>();
        }

        public OrderDetail(OrderExtra orderExtra)
        {
            this.orderExtra = orderExtra;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public virtual Advertisement Item { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public virtual Order Order { get; set; }

        public ICollection<OrderExtra> ProductOrderExtra { get; set; }

    }
}
