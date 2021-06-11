using Restaurant.ClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.PakClassified
{
    public class AdvertisementsHandler
    {

        public Advertisement GetAdvertisement(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Advertisements
                        .Include(a => a.Images)
                        .Include(a => a.Status)
                        .Include(a => a.Category)

                        where adv.Id == id
                        select adv).First();
            }
        }

        public AdvertisementImage GetAdvertisementImage(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.AdvertisementImages


                        where adv.FoodItem.Id == id
                        select adv).FirstOrDefault();
                //(from adv in context.Advertisements  where adv.Id==1 select adv).ToList();
            }
        }

        public Order GetUserOrder(int? id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                if (id == null)
                {
                    return (from adv in context.Orders
                            
                            select adv).OrderByDescending(x => x.Id).FirstOrDefault();
                }
                return (from adv in context.Orders
                        .Include(a => a.OrderDetail)
                        where adv.User.Id == id
                        select adv).OrderByDescending(x => x.Id).FirstOrDefault();// First();
                //(from adv in context.Advertisements  where adv.Id==1 select adv).ToList();
            }
        }

        public Order GetUserOrderWithId(int orderId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Orders

                        where  adv.Id == orderId
                        select adv).FirstOrDefault();// First();
                //(from adv in context.Advertisements  where adv.Id==1 select adv).ToList();
            }
        }
        public Order GetUserOrderWithZero(int orderId, int userId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Orders

                        where adv.User.Id == userId && adv.Id == orderId
                        select adv).FirstOrDefault();// First();
                //(from adv in context.Advertisements  where adv.Id==1 select adv).ToList();
            }
        }

        public Order GetUserOrderWithPrice(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Orders

                        where adv.User.Id == id && adv.Price != 0
                        select adv).OrderByDescending(x => x.Id).FirstOrDefault();// First();
                //(from adv in context.Advertisements  where adv.Id==1 select adv).ToList();
            }
        }

        public void AddOrder(Order CI)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                // context.Entry(adv.City).State = EntityState.Unchanged;
                //context.Entry(adv.Category).State = EntityState.Unchanged;
                //context.Entry(CI.OrderDetail).State = EntityState.Unchanged;
                //context.Entry(adv.Status).State = EntityState.Unchanged;
                context.Entry(CI.User).State = EntityState.Unchanged;
                context.Orders.Add(CI);
                context.SaveChanges();
            }
        }

        //public void AddOrderExtras(OrderDetail OD)
        //{
        //    using (PakClassifiedContext context = new PakClassifiedContext())
        //    {
        //       // context.Entry(OD.ProductId).State = EntityState.Unchanged;
        //        context.OrderDetails.Add(OD);
        //        context.SaveChanges();
        //    }
        //}

        public void AddOrderExtras(OrderExtra OE)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                context.Entry(OE.OrderDetail).State = EntityState.Unchanged;
                context.OrderExtras.Add(OE);
                context.SaveChanges();
            }
        }

        public void RemoveProduct(Advertisement adv)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {

                //context.Entry(adv.City).State = EntityState.Unchanged;
                //context.Entry(adv.SubCategory).State = EntityState.Unchanged;
                //context.Entry(adv.Type).State = EntityState.Unchanged;
                //context.Entry(adv.Status).State = EntityState.Unchanged;
                // context.Entry(adv.User).State = EntityState.Unchanged;
                
                context.Entry(adv).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void RemoveOrder(Order o)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                //context.Entry(order.User).State = EntityState.Unchanged;
                context.Entry(o).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public void RemoveCitem(OrderDetail cItem)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {             
                context.Entry(cItem).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public void RemoveItemExtras(OrderExtra eId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                context.Entry(eId).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public void EditStatus(Advertisement adv)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                context.Entry(adv).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void EditPrice(Order order)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {            
                //context.Entry(order.User).State = EntityState.Unchanged;

                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void EditQuantity(OrderDetail orderdetail)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {

                context.Entry(orderdetail).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void EditOrderStatus(Order order)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                context.Entry(order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public List<Advertisement> GetAdvertisements(AdvertisementStatus status)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                if (status == null)
                {
                    return (from adv in context.Advertisements
                        .Include(a => a.Images)
                        .Include(a => a.Status)
                        
                        select adv).ToList();
                }
                return (from adv in context.Advertisements                    
                        .Include(a => a.Images)
                        .Include(a => a.Status)
                        
                        where adv.Status.Id == status.Id
                        select adv).ToList();
            }
        }

        public List<Advertisement> GetFeaturedAdvertisements(AdvertisementStatus status)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Advertisements
                        .Include(a => a.Images)
                        .Include(a => a.Status)
                        
                        where adv.IsFeatured==true && adv.Status.Id == status.Id
                        select adv).ToList();
            }
        }

        public List<Order> GetOrders(int userId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders

                        where o.User.Id == userId && o.Price !=0
                        select o).OrderByDescending(x=>x.Id).ToList();
            }
        }

        public List<Feedback> GetFeedbacks()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from feed in context.Feedbacks
                        .Include(x=>x.User)
                        select feed).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Order> GetAllOrders()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders
                        .Include(a => a.User)
                        where o.Price > 0
                        select o).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Order> GetAllDeliveredOrders()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders
                        .Include(a => a.User)
                        where o.Price > 0 && o.Status == "Delivered"
                        select o).OrderByDescending(x => x.Id).ToList();
            }
        }



        public List<Order> TodaysOrders()
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders
                        where (o.AddedOn >= startDateTime && o.AddedOn <= endDateTime)
                      
                        select o).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Order> GetAllOrdersWithStatus()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders
                        .Include(a => a.User)
                        where o.Price > 0 && (o.Status == "Waiting for Review" || o.Status== "Accepted")
                        select o).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Order> OrdersDelivered()
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders
                        .Include(a => a.User)
                        where o.Price > 0 && o.Status == "Delivered" && (o.AddedOn >= startDateTime && o.AddedOn <= endDateTime)
                        select o).OrderByDescending(x => x.Id).ToList();
            }
        }

        public List<Order> GetOrdersWithZero(int userId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from o in context.Orders

                        where o.User.Id == userId && o.Price == 0
                        select o).ToList();
            }
        }



        public List<OrderDetail> GetCartedItems(int orderId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from cItem in context.OrderDetails                    

                        where cItem.Order.Id== orderId
                        select cItem).ToList();
            }
        }

        public List<OrderExtra> GetItemExtras(int itemId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from e in context.OrderExtras

                        where e.OrderDetail.Id == itemId && e.IsChecked == true
                        select e).ToList();
            }
        }

        public List<OrderExtra> GetExtras(int cItemId)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from e in context.OrderExtras

                        where e.OrderDetail.Id == cItemId
                        select e).ToList();
            }
        }

        public Extras GetExtraById(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from e in context.Extrass

                        where e.Id == id
                        select e).FirstOrDefault();
            }
        }

        public OrderDetail GetCartedItemById(int id)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from cItem in context.OrderDetails

                        where cItem.Id == id
                        select cItem).FirstOrDefault();
            }
        }

        public List<Advertisement> GetLatestAdvertisements(int count,AdvertisementStatus status)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Advertisements 
                        .Include(a => a.Images)
                        .Include(a => a.Status)
                        
                        where adv.Status.Id == status.Id
                        orderby adv.AddedOn
                        select adv).Take(count).ToList();
            }
        }

        public List<Advertisement> GetAdvertisementsByCategory(Category category,AdvertisementStatus status)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from adv in context.Advertisements
                        .Include(a => a.Images)
                        .Include(a => a.Status)
                        
                        where adv.Category.Id == category.Id && adv.Status.Id == status.Id
                        select adv).ToList();
            }
        }

        public List<Extras> GetExtrasByCategory(Category category)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from e in context.Extrass
                        //.Include(a => a.ExtraNutritionInfo)
                        where e.Categories.Id == category.Id
                        select e).ToList();
              
            }
        }

        public void Add(Advertisement adv)
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
               // context.Entry(adv.City).State = EntityState.Unchanged;
               context.Entry(adv.Category).State = EntityState.Unchanged;
                
                context.Entry(adv.Status).State = EntityState.Unchanged;
               // context.Entry(adv.User).State = EntityState.Unchanged;
                context.Advertisements.Add(adv);
                context.SaveChanges();
            }
        }

        public List<Category> GetCategories()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from c in context.Categories
                        select c).ToList();
            }
        }


       


        //public List<SubCategory> GetSubCategories(Category category)
        //{
        //    using (PakClassifiedContext context = new PakClassifiedContext())
        //    {
        //        return (from sc in context.SubCategories
        //                .Include("Category")
        //                where sc.Category.Id == category.Id
        //                select sc).ToList();
        //    }
        //}

        public List<AdvertisementStatus> GetStatus()
        {
            using (PakClassifiedContext context = new PakClassifiedContext())
            {
                return (from t in context.Status
                        select t).ToList();
            }
        }
    }
}
