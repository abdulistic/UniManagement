using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class DashboardModel
    {
        public int AllOrders { get; set; }
        public int DeliveredOrders { get; set; }
        public int OrderRatio { get; set; }
        public int TodOrders { get; set; }
        public int TodOrdersDelivered { get; set; }
        public int Customers { get; set; }
        public float TotalRev { get; set; }
        public float TodaysRev { get; set; }
    }
}