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
    public class ProductsController : Controller
    {
        public static List<FormCollection> cartList = new List<FormCollection>();

        // GET: Products
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult EditItem(int id)
        {

            Advertisement item = new AdvertisementsHandler().GetAdvertisement(id);
            EditItemModel edit = new EditItemModel() { Id = item.Id, Name = item.Title, Status = item.Status.Id, Price = item.Price, IsFeatured = item.IsFeatured};

            return PartialView("~/Views/Shared/_EditItemPartialView.cshtml", edit);
        }



        public ActionResult extraNutritionDetail(int id)
        {
            Extras e = new AdvertisementsHandler().GetExtraById(id);
            ChefDetailExtrasModel model = new ChefDetailExtrasModel();
            model.Id = e.Id;
            model.Name = e.Name;
            model.Protein = e.Protein;
            model.SaturatedFat = e.SaturatedFat;
            model.Sodium = e.Sodium;
            model.Sugar = e.Sugar;
            model.TransFat = e.TransFat;
            model.Fiber = e.Fiber;
            model.Carbohydrates = e.Carbohydrates;
            model.Calories = e.Calories;
            model.Cholestrol = e.Cholestrol;
            model.Fat = e.Fat;

            // ViewBag.ExtraNutrition = model;
            return PartialView("~/Views/Shared/_ExtraNutritionPartialView.cshtml", model);
        }

        [HttpGet]
        public ActionResult ViewDetails(int id)
        {
            Advertisement adv = new AdvertisementsHandler().GetAdvertisement(id);
            AdvertisementImage img = new AdvertisementsHandler().GetAdvertisementImage(adv.Id);
            int catgoryId = adv.Category.Id;
            var extras = new AdvertisementsHandler().GetExtrasByCategory(new Category { Id = catgoryId });

            ProductDetailsModel model = new ProductDetailsModel();
            model.Id = adv.Id;
            model.Title = adv.Title;
            model.Price = adv.Price;
            model.Description = adv.Description;
            model.ImageUrl = img.Url;

            model.Calories = adv.Calories;
            model.Carbohydrates = adv.Carbohydrates;
            model.Cholestrol = adv.Cholestrol;
            model.Fat = adv.Fat;
            model.Fiber = adv.Fiber;
            model.Protein = adv.Protein;
            model.SaturatedFat = adv.SaturatedFat;
            model.Sodium = adv.Sodium;
            model.Sugar = adv.Sugar;
            model.TransFat = adv.TransFat;
            foreach (var item in extras)
            {
                model.DetailExtrasModel.Add(new DetailExtrasModel { Id = item.Id, Name = item.Name, Price = item.Price });
            }
            return PartialView("~/Views/Shared/_DetailPartialView.cshtml", model);
        }

        [HttpGet]
        public ActionResult CheckOut()
        {
            User cUser = (User)Session[WebUtil.CurrentUser];
            if (!(cUser != null)) return RedirectToAction("Login", "Users", new { returnUrl = "products/checkout" });
            Order objOrder = new Order();
            OrderDetail objOrderDetail = new OrderDetail();
            OrderExtra oOrderExtra = new OrderExtra();
            City objCity = new City();

            List<int> CartIds = new List<int>();
            List<int> ExtraIds = new List<int>();
            List<string> CartNames = new List<string>();
            List<bool> CartIsChecked = new List<bool>();


            objOrder.AddedOn = Convert.ToDateTime("2015-03-19 22:51:34.237");
            objOrder.User = cUser;


            foreach (var item in cartList)
            {
                objOrder.OrderDetail.Add(new OrderDetail { ProductId = Convert.ToInt32(item["id"]), Name = item["name"] });
            }
            new AdvertisementsHandler().AddOrder(objOrder);
            var aa = new PakClassifiedContext().Orders.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.CartItems = new AdvertisementsHandler().GetCartedItems(aa.Id);
            var cItems = ViewBag.CartItems;
            var checkoutList = cartList;
            foreach (var item in cItems)
            {

                Advertisement adv = new AdvertisementsHandler().GetAdvertisement(item.ProductId);
                int catgoryId = adv.Category.Id;
                ViewBag.RecipeExtras = new AdvertisementsHandler().GetExtrasByCategory(new Category { Id = catgoryId }).ToSelectItemList();
                List<SelectListItem> objExtras = ViewBag.RecipeExtras;
                foreach (var extras in objExtras)
                {

                    CartIds.Add(item.Id);

                }
            }




            foreach (var item in cartList)
            {

                Advertisement adv = new AdvertisementsHandler().GetAdvertisement(Convert.ToInt32(item["id"]));
                int catgoryId = adv.Category.Id;
                var extras = new AdvertisementsHandler().GetExtrasByCategory(new Category { Id = catgoryId });


                foreach (var e in extras)
                {
                    ExtraIds.Add(e.Id);
                    CartNames.Add(e.Name);
                    try
                    {
                        CartIsChecked.Add(Convert.ToBoolean(item[e.Name].Split(',').First()));

                    }
                    catch
                    {
                        CartIsChecked.Add(false);

                    }


                }
            }



            int[] aCartIds = CartIds.ToArray();
            int[] aExtraIds = ExtraIds.ToArray();
            string[] aCartNames = CartNames.ToArray();
            bool[] aCartIsChecked = CartIsChecked.ToArray();

            for (int i = 0; i < aCartIsChecked.Length; i++)
            {
                oOrderExtra.OrderDetail = new OrderDetail { Id = aCartIds[i] };
                oOrderExtra.ItemId = aCartIds[i];
                oOrderExtra.ExtraId = aExtraIds[i];
                oOrderExtra.Name = aCartNames[i];
                oOrderExtra.IsChecked = aCartIsChecked[i];
                new AdvertisementsHandler().AddOrderExtras(oOrderExtra);

            }

            //cartList.Clear();
            return RedirectToAction("checkout", "home");
        }


        [HttpPost]
        public ActionResult AddToCart(FormCollection data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            try
            {
                List<Order> order = new AdvertisementsHandler().GetOrdersWithZero(user.Id);
                if (order.Count > 0)
                {
                    foreach (var o in order)
                    {
                        List<OrderDetail> oDetail = new AdvertisementsHandler().GetCartedItems(o.Id);
                        foreach (var d in oDetail)
                        {
                            ViewBag.ItemExtras = new AdvertisementsHandler().GetExtras(d.Id);
                            var iExtras = ViewBag.ItemExtras;
                            foreach (var extras in iExtras)
                            {
                                new AdvertisementsHandler().RemoveItemExtras(extras);
                            }
                        }
                    }

                    foreach (var oD in order)
                    {
                        List<OrderDetail> oDetail = new AdvertisementsHandler().GetCartedItems(oD.Id);
                        foreach (var d in oDetail)
                        {
                            new AdvertisementsHandler().RemoveCitem(d);
                        }
                    }

                    foreach (var o in order)
                    {
                        new AdvertisementsHandler().RemoveOrder(o);
                    }
                }
                cartList.Add(data);
                return RedirectToAction("ByCategory", "home");
            }
            catch (Exception)
            {
                cartList.Add(data);
                return RedirectToAction("ByCategory", "home");
            }



        }

        [HttpPost]
        public ActionResult AddTotalPrice(FormCollection data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            float price = Convert.ToSingle(data["total"]);
            if (price > 0)
            {
                DateTime prevDate = Convert.ToDateTime("2015-03-19 22:51:34.237");
                Order o = new AdvertisementsHandler().GetUserOrder(user.Id);
                o.Price = price;
                o.Status = WebUtil.WaitingForReview;
                if (o.AddedOn == prevDate)
                {
                    o.AddedOn = DateTime.Now;
                }

                string s1 = data["quantities"];
                string sQuantity = s1.TrimEnd(',');

                List<int> quantity = new List<int>(Array.ConvertAll(sQuantity.Split(','), int.Parse));

                List<OrderDetail> cartItems = new AdvertisementsHandler().GetCartedItems(o.Id);

                for (int i = 0; i < quantity.Count(); i++)
                {
                    cartItems[i].Quantity = quantity[i];
                    new AdvertisementsHandler().EditQuantity(cartItems[i]);
                }

                new AdvertisementsHandler().EditPrice(o);
            }

            return RedirectToAction("paymentwithpaypal", "paypal");
        }

        [HttpGet]
        public ActionResult StatusAccepted(int id)
        {
            Order o = new AdvertisementsHandler().GetUserOrderWithId(id);
            o.Status = WebUtil.Accepted;
            new AdvertisementsHandler().EditOrderStatus(o);

            return RedirectToAction("orders", "products");
        }

        [HttpGet]
        public ActionResult StatusDelivered(int id)
        {
            Order o = new AdvertisementsHandler().GetUserOrderWithId(id);
            o.Status = WebUtil.Delivered;
            new AdvertisementsHandler().EditOrderStatus(o);

            return RedirectToAction("orders", "products");
        }

        [HttpGet]
        public ActionResult StatusDeclined(int id)
        {
            Order o = new AdvertisementsHandler().GetUserOrderWithId(id);
            o.Status = WebUtil.Declined;
            new AdvertisementsHandler().EditOrderStatus(o);

            return RedirectToAction("orders", "products");
        }


        [HttpGet]
        public ActionResult CartItem(string id)
        {
            string tId = id.TrimEnd();
            string[] parts = new string[tId.Length];
            if (!string.IsNullOrWhiteSpace(tId))
            {
                parts = tId.Split(',');

            }
            int[] clist = new int[parts.Length - 1];
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (!string.IsNullOrWhiteSpace(parts[i]))
                {
                    clist[i] = Convert.ToInt32(parts[i]);

                }

            }

            List<int> clist1 = new List<int>();

            List<int> quantity1 = new List<int>();

            for (int i = 0; i < clist.Length; i++)
            {
                if (!(clist1.Contains(clist[i])))
                {
                    clist1.Add(clist[i]);
                }

            }
            int[] alist = clist1.ToArray();
            for (int i = 0; i < alist.Length; i++)
            {
                int quantity = 0;
                for (int j = 0; j < clist.Length; j++)
                {

                    if (alist[i] == clist[j])
                    {
                        quantity++;
                    }
                }
                quantity1.Add(quantity);
            }
            int[] aquantity = quantity1.ToArray();
            User cUser = (User)Session[WebUtil.CurrentUser];
            Order objOrder = new Order();
            objOrder.AddedOn = DateTime.Now;
            objOrder.User = cUser;

            for (int i = 0; i < aquantity.Length; i++)
            {
                objOrder.OrderDetail.Add(new OrderDetail { ProductId = alist[i], Quantity = aquantity[i] });

            }


            new AdvertisementsHandler().AddOrder(objOrder);
            return RedirectToAction("home", "home");
        }

        [HttpGet]
        public ActionResult DelCartItem(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            ViewBag.ItemExtras = new AdvertisementsHandler().GetExtras(id);
            var iExtras = ViewBag.ItemExtras;
            foreach (var extras in iExtras)
            {
                new AdvertisementsHandler().RemoveItemExtras(extras);
            }

            OrderDetail cItem = new AdvertisementsHandler().GetCartedItemById(id);
            
            new AdvertisementsHandler().RemoveCitem(cItem);


            Order order = new PakClassifiedContext().Orders.OrderByDescending(x => x.Id).FirstOrDefault();
            List<OrderDetail> obj = new AdvertisementsHandler().GetCartedItems(order.Id);
            var cItems = obj;
            if (obj.Count == 0)
            {
                Order o = new AdvertisementsHandler().GetUserOrder(user.Id);
                new AdvertisementsHandler().RemoveOrder(o);
            }

           
            int i = 0;
            foreach (var item in cartList)
            {
                
                if (Convert.ToInt32(item["id"]) == cItem.ProductId)
                {
                    cartList.RemoveAt(i);
                    break;
                }
                i++;
            }

            return RedirectToAction("checkout", "home");
        }


        [HttpGet]
        public ActionResult DelProduct(int id)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/manageproduct" });
            Advertisement adv = new AdvertisementsHandler().GetAdvertisement(id);

            new AdvertisementsHandler().RemoveProduct(adv);
            return RedirectToAction("ManageProduct");
        }

        [HttpPost]
        public ActionResult EditFoodItem(FormCollection form)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/manageproduct" });
            Advertisement adv = new AdvertisementsHandler().GetAdvertisement(Convert.ToInt32(form["Id"]));
            if (!(string.IsNullOrEmpty( form["Name"])))
            {
                adv.Title = form["Name"];
            }

            if (!(string.IsNullOrEmpty(form["Price"])))
            {
                adv.Price =float.Parse(form["Price"]);
            }

            if (!(string.IsNullOrEmpty(form["Status"])))
            {
                adv.Status = new AdvertisementStatus { Id = Convert.ToInt32(form["Status"]) };
                adv.Status_Id = Convert.ToInt32(form["Status"]);

            }

            if (!(string.IsNullOrEmpty(form["IsFeatured"])))
            {
                adv.IsFeatured = Convert.ToBoolean( form["IsFeatured"].Split(',').First()); 
            }

            new AdvertisementsHandler().EditStatus(adv);
            return RedirectToAction("ManageProduct");
        }




        //[HttpGet]
        //public ActionResult Available(int id)
        //{
        //    User user = (User)Session[WebUtil.CurrentUser];
        //    if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/manageproduct" });
        //    Advertisement adv = new AdvertisementsHandler().GetAdvertisement(id);
        //    adv.Status = new AdvertisementStatus { Id = 1 };
        //    adv.Status_Id = 1;
        //    new AdvertisementsHandler().EditStatus(adv);
        //    return RedirectToAction("ManageProduct");
        //}
        //[HttpGet]
        //public ActionResult Unavailable(int id)
        //{
        //    User user = (User)Session[WebUtil.CurrentUser];
        //    if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/manageproduct" });
        //    Advertisement adv = new AdvertisementsHandler().GetAdvertisement(id);
        //    adv.Status = new AdvertisementStatus { Id = 2 };
        //    adv.Status_Id = 2;
        //    new AdvertisementsHandler().EditStatus(adv);
        //    return RedirectToAction("ManageProduct");
        //}


        [HttpGet]
        public ActionResult ManageProduct()
        {
            List<FeaturedRecipesModel> model = new List<FeaturedRecipesModel>();
            try
            {
                User user = (User)Session[WebUtil.CurrentUser];
                if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/manageproduct" });
                List<Advertisement> advertisements = new AdvertisementsHandler().GetAdvertisements(null);

                foreach (var adv in advertisements)
                {
                    FeaturedRecipesModel m = new FeaturedRecipesModel();
                    m.Id = adv.Id;
                    m.Price = adv.Price;
                    m.Title = adv.Title;
                    m.ImageUrl = (adv.Images.Count > 0) ? adv.Images.First().Url : "/images/temp/nophoto.png";
                    m.Status = new AdvStatusModel(adv.Status);
                    model.Add(m);
                }
            }
            catch (Exception x)
            {
                Console.WriteLine("error" + x);
            }
            return View(model);


        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/addproduct" });

            ViewBag.Categories = new AdvertisementsHandler().GetCategories().ToSelectItemList();
            List<SelectListItem> status = new AdvertisementsHandler().GetStatus().ToSelectItemList();
            ViewBag.AdvTypes = status;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(AddProductModel data)
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/addproduct" });

            Advertisement adv = new Advertisement();

            adv.Title = data.Title;
            adv.Price = Convert.ToSingle(data.Price);
            adv.Category = new Category { Id = Convert.ToInt32(data.Category) };
            adv.Description = data.Description;
            adv.IsFeatured = Convert.ToBoolean(data.IsFeatured);
            adv.Status = new AdvertisementStatus { Id = Convert.ToInt32(data.Status) };
            adv.AddedOn = DateTime.Now;

            //nutritionInfo
            adv.Calories = Convert.ToSingle(data.Calories);
            adv.Fat = Convert.ToSingle(data.Fat);
            adv.SaturatedFat = Convert.ToSingle(data.SaturatedFat);
            adv.TransFat = Convert.ToSingle(data.TransFat);
            adv.Cholestrol = Convert.ToSingle(data.Cholestrol);
            adv.Sodium = Convert.ToSingle(data.Sodium);
            adv.Carbohydrates = Convert.ToSingle(data.Carbohydrates);
            adv.Fiber = Convert.ToSingle(data.Fiber);
            adv.Sugar = Convert.ToSingle(data.Sugar);
            adv.Protein = Convert.ToSingle(data.Protein);


            int counter = 0;
            long uno = DateTime.Now.Ticks;

            HttpPostedFileBase file = Request.Files[0];

            string url = "/dataimages/products/" + $"{uno}_{++counter}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
            string path = Request.MapPath(url);
            file.SaveAs(path);
            adv.Images.Add(new AdvertisementImage { Url = url });

            new AdvertisementsHandler().Add(adv);
            //TempData.Add("Message", new AlertModel("The advertisement added successfully", AlertType.Success));


            return RedirectToAction("AddProduct");
        }


        [HttpGet]
        public ActionResult Orders()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.CHEF_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/orders" });
            List<ChefOrderModel> model = new List<ChefOrderModel>();
            List<ChefOrderDetailModel> odmodel = new List<ChefOrderDetailModel>();
            List<ChefDetailExtrasModel> demodel = new List<ChefDetailExtrasModel>();

            List<Order> order = new AdvertisementsHandler().GetAllOrdersWithStatus();
            // ViewBag.AllOrders = new AdvertisementsHandler().GetAllOrders().ToChefOrderModelList();

            foreach (var o in order)
            {
                ChefOrderModel m = new ChefOrderModel();
                m.Id = o.Id;
                m.Price = o.Price;
                m.Date  = o.AddedOn.ToShortDateString();
                m.Time = o.AddedOn.ToShortTimeString();
                m.Status = o.Status;
                m.CustomerId = o.User.Id;
                m.Address = o.User.Address;
                m.UserName = o.User.FullName;
                m.Mobile = o.User.ContactNumber;
                List<OrderDetail> odetail = new AdvertisementsHandler().GetCartedItems(o.Id);
                foreach (var od in odetail)
                {
                    var extralist = "";
                    var extras = new AdvertisementsHandler().GetItemExtras(od.Id);
                    foreach (var e in extras)
                    {
                        extralist += e.Name + "  |  ";
                    }
                    extralist.TrimEnd();
                    string extralist1 = extralist.TrimEnd('|');
                    m.ChefOrderItem.Add(new ChefOrderDetailModel { Id = od.Id, Title = od.Name,Quantity=od.Quantity ,Extras = extralist1 });
                }

                model.Add(m);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Statistics()
        {
            User user = (User)Session[WebUtil.CurrentUser];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/statistics" });
            List<AdminOrderModel> model = new List<AdminOrderModel>();
            List<AdminOrderDetailModel> odmodel = new List<AdminOrderDetailModel>();
            List<ChefDetailExtrasModel> demodel = new List<ChefDetailExtrasModel>();

            List<Order> order = new AdvertisementsHandler().GetAllOrders();
            // ViewBag.AllOrders = new AdvertisementsHandler().GetAllOrders().ToChefOrderModelList();

            foreach (var o in order)
            {
                AdminOrderModel m = new AdminOrderModel();
                m.Id = o.Id;
                m.Price = o.Price;
                m.Time = o.AddedOn.ToShortTimeString();
                m.Date = o.AddedOn.ToShortDateString();
                m.Status = o.Status;
                m.CustomerId = o.User.Id;
                m.Address = o.User.Address;
                m.UserName = o.User.FullName;
                List<OrderDetail> odetail = new AdvertisementsHandler().GetCartedItems(o.Id);
                foreach (var od in odetail)
                {
                    var extralist = "";
                    var extras = new AdvertisementsHandler().GetItemExtras(od.Id);
                    foreach (var e in extras)
                    {
                        extralist += e.Name + "  |  ";
                    }
                    extralist.TrimEnd();
                    string extralist1 = extralist.TrimEnd('|');
                    m.AdminOrderItem.Add(new AdminOrderDetailModel { Id = od.Id, Title = od.Name, Quantity=od.Quantity ,Extras = extralist1 });
                }

                model.Add(m);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult UserFeedback()
        {
            User cUser = (User)Session[WebUtil.CurrentUser];
            if (!(cUser != null && cUser.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "products/userfeedback" });

            var feedback = new AdvertisementsHandler().GetFeedbacks();
            List<FeedbackModel> model = new List<FeedbackModel>();
            foreach (var fb in feedback)
            {
                var user = new UsersHandler().GetUserWithId(fb.User.Id);
                FeedbackModel m = new FeedbackModel();
                m.Name = user.FullName;
                m.SubmittedOn = fb.SubmittedOn;
                m.Email = user.Email;
                m.Conatact = user.ContactNumber;
                m.Message = fb.UserFeedback;
                model.Add(m);
            }

            return View(model);
        }


      


    }
}