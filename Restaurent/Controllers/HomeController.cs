using Restaurent.Models;
using Restaurant.ClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.ClassLibrary.PakClassified;
using Restaurant.ClassLibrary;

namespace Restaurent.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Home()
        {
            ViewBag.FeaturedAdvertisements = new AdvertisementsHandler().GetFeaturedAdvertisements(new AdvertisementStatus { Id = 1 }).ToAdvSummaryModelList();
            return View();
        }

        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactUs(FormCollection data)
        {
            User cUser = (User)Session[WebUtil.CurrentUser];
            if (!(cUser != null)) return RedirectToAction("Login", "Users", new { returnUrl = "home/contactus" });
            Feedback feed = new Feedback();
            feed.UserFeedback = data["Message"];
            feed.User = cUser;
            feed.SubmittedOn = DateTime.Now;
            new UsersHandler().AddFeedback(feed);
            return View();
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            User cUser = (User)Session[WebUtil.CurrentUser];
            if (!(cUser != null)) return RedirectToAction("Login", "Users", new { returnUrl = "home/checkout" });
            try
            {
                Order o = new AdvertisementsHandler().GetUserOrder(cUser.Id);
                if (o.Price == 0)
                {
                    var aa = new PakClassifiedContext().Orders.OrderByDescending(x => x.Id).FirstOrDefault();
                    var cItems = new AdvertisementsHandler().GetCartedItems(aa.Id);
                    
                    List<CheckoutModel> model = new List<CheckoutModel>();

                    foreach (var item in cItems)
                    {
                        Advertisement adv = new AdvertisementsHandler().GetAdvertisement(item.ProductId);

                        var mPrice = adv.Price;
                        var mCal = adv.Calories;
                        var mCar = adv.Carbohydrates;
                        var mCho = adv.Cholestrol;
                        var mFat = adv.Fat;
                        var mFib = adv.Fiber;
                        var mPro = adv.Protein;
                        var mSat = adv.SaturatedFat;
                        var mSod = adv.Sodium;
                        var mSug = adv.Sugar;
                        var mTra = adv.TransFat;

                        try
                        {
                            var extras = new AdvertisementsHandler().GetItemExtras(item.Id);
                            foreach (var e in extras)
                            {
                                var ext = new AdvertisementsHandler().GetExtraById(e.ExtraId);
                                mPrice += ext.Price;

                                mCal += ext.Calories;
                                mCar += ext.Carbohydrates;
                                mCho += ext.Cholestrol;
                                mFat += ext.Fat;
                                mFib += ext.Fiber;
                                mPro += ext.Protein;
                                mSat += ext.SaturatedFat;
                                mSod += ext.Sodium;
                                mSug += ext.Sugar;
                                mTra += ext.TransFat;
                            }

                            CheckoutModel m = new CheckoutModel();
                            m.Id = adv.Id;
                            m.CartItemId = item.Id;
                            m.Price = mPrice;
                            m.Title = adv.Title;
                            m.ImageUrl = (adv.Images.Count > 0) ? adv.Images.First().Url : "/images/temp/nophoto.png";
                            m.Calories = mCal;
                            m.Carbohydrates = mCar;
                            m.Cholestrol = mCho;
                            m.Fat = mFat;
                            m.Fiber = mFib;
                            m.Protein = mPro;
                            m.SaturatedFat = mSat;
                            m.Sodium = mSod;
                            m.Sugar = mSug;
                            m.TransFat = mTra;
                            foreach (var ex in extras)
                            {
                                m.ItemExtras.Add(new DetailExtrasModel { Name = ex.Name });
                            }
                            
                            model.Add(m);
                        }
                        catch (Exception)
                        {
                            CheckoutModel m = new CheckoutModel();
                            m.Id = adv.Id;
                            m.CartItemId = item.Id;
                            m.Price = mPrice;
                            m.Title = adv.Title;
                            m.ImageUrl = (adv.Images.Count > 0) ? adv.Images.First().Url : "/images/temp/nophoto.png";
                            m.Calories = mCal;
                            m.Carbohydrates = mCar;
                            m.Cholestrol = mCho;
                            m.Fat = mFat;
                            m.Fiber = mFib;
                            m.Protein = mPro;
                            m.SaturatedFat = mSat;
                            m.Sodium = mSod;
                            m.Sugar = mSug;
                            m.TransFat = mTra;
                            model.Add(m);
                        }              

                     
                    }

                    return View(model);

                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {

                return View();
            }
           
         
           
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }



        [HttpGet]
        public ActionResult ByCategory()
        {
            var CartetItems = ProductsController.cartList;
            if (!(CartetItems == null))
            {
                List<CartedItemsModel> model = new List<CartedItemsModel>();
                foreach (var item in CartetItems)
                {
                    CartedItemsModel m = new CartedItemsModel();
                    Advertisement advertisement = new AdvertisementsHandler().GetAdvertisement(Convert.ToInt32(item["id"]));

                    m.Name = advertisement.Title;
                    m.Image = (advertisement.Images.Count() > 0) ? advertisement.Images.First().Url : "/images/temp/nophoto.png";
                    model.Add(m);
                }
                ViewBag.CartedItems = model;
            }

            ViewBag.Advertisements = new AdvertisementsHandler().GetAdvertisementsByCategory(new Category { Id = 1 }, new AdvertisementStatus { Id = 1 }).ToAdvSummaryModelList();
            //ViewBag.Extras = new AdvertisementsHandler().GetExtrasByCategory(new Category { Id = 1 }).ToSelectItemList();
            List<FeaturedRecipesModel> objCategory = (List<FeaturedRecipesModel>)ViewBag.Advertisements;
            //return PartialView("~/Views/Shared/_CategoryPartialView.cshtml", objCategory);
            return View();
        }


        [HttpGet]
        public ActionResult Category(int id)
        {
            ViewBag.Advertisements = new AdvertisementsHandler().GetAdvertisementsByCategory(new Category { Id = id }, new AdvertisementStatus { Id = 1 }).ToAdvSummaryModelList();
            //ViewBag.Extras = new AdvertisementsHandler().GetExtrasByCategory(new Category { Id = id }).ToSelectItemList();
            List<FeaturedRecipesModel> objCategory = (List<FeaturedRecipesModel>)ViewBag.Advertisements;
            return PartialView("~/Views/Shared/_CategoryPartialView.cshtml", objCategory);
        }

        [HttpGet]
        public ActionResult Test2()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Admin()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "home/admin" });
            List<Order> order = new AdvertisementsHandler().GetAllOrders();
            List<Order> todaysOrders = new AdvertisementsHandler().TodaysOrders();
            List<Order> ordersDelivered = new AdvertisementsHandler().OrdersDelivered();
            List<Order> ordersDeliveredAll = new AdvertisementsHandler().GetAllDeliveredOrders();
            List<User> allUsers = new UsersHandler().GetUsers();
            int allOrders=0;
            int allDeliveredOrders=0;
            int tOrders=0;
            int tOrdersDelivered=0;
            int totalUsers=0;
            float totalRevenue=0;
            float todaysRevenue=0;
            Double ratio=0;
            foreach (var o in order)
            {
                totalRevenue += o.Price;
                allOrders++;
            }

            foreach (var tO in todaysOrders)
            {
                todaysRevenue += tO.Price;
                tOrders++;
            }
            foreach (var u in allUsers)
            {
                totalUsers++;
            }
            foreach (var tOD in ordersDelivered)
            {
                tOrdersDelivered++;
            }
            foreach (var item in ordersDeliveredAll)
            {
                allDeliveredOrders++;
            }
            ratio = (int)Math.Round((double)(100 * allDeliveredOrders) / allOrders);

            DashboardModel model = new DashboardModel();
            model.AllOrders = allOrders;
            model.Customers = totalUsers;
            model.TotalRev = totalRevenue;
            model.TodaysRev = todaysRevenue;
            model.TodOrders = tOrders;
            model.TodOrdersDelivered = tOrdersDelivered;
            model.DeliveredOrders = allDeliveredOrders;
            model.OrderRatio =Convert.ToInt32(ratio);

            return View(model);
        }

        [HttpGet]
        public ActionResult Chef()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.CHEF_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "home/chef" });

            return View();
        }

        [HttpGet]
        public ActionResult ConfirmOrder()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            User usertoGet = new UsersHandler().GetUserWithId(user.Id);
            if (!(user != null)) return RedirectToAction("Login", "Users", new { returnUrl = "home/checkout" });
          
          
            List<Order> order = new AdvertisementsHandler().GetOrders(user.Id);
            List<OrderModel> model = new List<OrderModel>();
            

            foreach (var item in order)
            {
                OrderModel m = new OrderModel();
                m.Id = item.Id;
                m.Price = item.Price;
                m.Status = item.Status;
                m.dateTime = item.AddedOn;
                m.Address = user.Address;
                m.City = usertoGet.City.Name;
                m.State = usertoGet.City.Province.Name;
                m.Country = usertoGet.City.Province.Country.Name;
                model.Add(m);
            }
            ViewBag.Order = new AdvertisementsHandler().GetUserOrderWithPrice(user.Id);


            ProductsController.cartList.Clear();
            return View(model);
        }

        [HttpGet]
        public ActionResult OrderHistory()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            User userGet = new UsersHandler().GetUserWithId(user.Id); 
            if (!(user != null)) return RedirectToAction("Login", "Users", new { returnUrl = "home/orderhistory" });           
            List<Order> order = new AdvertisementsHandler().GetOrders(user.Id);
          
            List<OrderModel> model = new List<OrderModel>();

            foreach (var item in order)
            {
                OrderModel m = new OrderModel();
                m.Id = item.Id;
                m.Price = item.Price;
                m.Status = item.Status;
                m.dateTime = item.AddedOn;
                m.Address = userGet.Address;
                m.City = userGet.City.Name;
                m.Country = userGet.City.Province.Country.Name;
                model.Add(m);
            }
            ViewBag.Order = new AdvertisementsHandler().GetUserOrderWithPrice(user.Id);

            return View(model);
        }


    }
}