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
    public class AdvertisementsController : Controller
    {
        [HttpGet]
        public ActionResult Manage()
        {
            User user = (User)Session[WebUtil.CURRENT_USER];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "advertisements/manage" });
            List<Advertisement> advertisements = new AdvertisementsHandler().GetAdvertisements(null);
            List<AdvSummaryModel> model = new List<AdvSummaryModel>();
            foreach (var adv in advertisements)
            {
                AdvSummaryModel m = new AdvSummaryModel();
                m.Id = adv.Id;
                m.Price = adv.Price;
                m.Title = adv.Title;
                m.ImageUrl = (adv.Images.Count > 0) ? adv.Images.First().Url : "/images/temp/nophoto.png";
                m.Status = new AdvStatusModel(adv.Status);
                model.Add(m);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult ApproveReject(int id)
        {
            User user = (User)Session[WebUtil.CURRENT_USER];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "advertisements/manage" });
            Advertisement adv = new AdvertisementsHandler().GetAdvertisement(id);
            AdvDetailsModel model = new AdvDetailsModel();
            model.Id = adv.Id;
            model.Title = adv.Title;
            model.Description = adv.Description;
            model.Price = adv.Price;
            foreach (var img in adv.Images)
            {
                model.ImageUrls.Add(img.Url);
            }
            return View("~/Views/Advertisements/_ApproveRejectView.cshtml", model);
        }



        [HttpPost]
        public ActionResult ApproveReject(ApproveRejectModel model)
        {
            User user = (User)Session[WebUtil.CURRENT_USER];
            if (!(user != null && user.IsInRole(WebUtil.ADMIN_ROLE))) return RedirectToAction("Login", "Users", new { returnUrl = "advertisements/manage" });
            //code to approve or reject advertisements
            return RedirectToAction("Manage");
        }



        [HttpGet]
        public ActionResult Featured()
        {
           // ViewBag.FeaturedAdvertisements = new AdvertisementsHandler().GetFeaturedAdvertisements(new AdvertisementStatus { Id = 1 }).ToAdvSummaryModelList();
            return View();
        }

        [HttpGet]
        public ActionResult ByCategory()
        {
            List<FeaturedRecipesModel> objAdvList = new List<FeaturedRecipesModel>();
            objAdvList.Add(new FeaturedRecipesModel { Id = 1, Title = "Samasung Galaxy S8", Price = 45000,  ImageUrl = "/Images/Restaursnt Recipes pics/20161201-crispy-roast-potatoes-29-thumb-1500xauto-435281.jpg" });
            objAdvList.Add(new FeaturedRecipesModel { Id = 2, Title = "Huawei Mate 10", Price = 35000, ImageUrl = "/Images/Restaursnt Recipes pics/20161201-crispy-roast-potatoes-29-thumb-1500xauto-435281.jpg" });
            objAdvList.Add(new FeaturedRecipesModel { Id = 3, Title = "Apple iPhone X", Price = 75000, ImageUrl = "/Images/Restaursnt Recipes pics/20161201-crispy-roast-potatoes-29-thumb-1500xauto-435281.jpg" });
            //objAdvList.Add(new FeaturedRecipesModel { Id = 4, Title = "Apple iPhone 8", Price = 85000, ImgURL = "/Images/temp/s2.jpg" });
            //objAdvList.Add(new FeaturedRecipesModel { Id = 5, Title = "Google Pixel 2", Price = 55000, ImgURL = "/Images/temp/s3.jpg" });
            ViewBag.AdvertisementList = objAdvList;
            return View();
            //ViewBag.Advertisements = new AdvertisementsHandler().GetAdvertisementsByCategory(new Category { Id = id }, new AdvertisementStatus { Id = 1 }).ToAdvSummaryModelList();
            //return View();
        }

        [HttpGet]
        public ActionResult PostAdv()
        {
            User cUser = (User)Session[WebUtil.CURRENT_USER];
            if (cUser == null) return RedirectToAction("Login", "Users", new { returnUrl = "advertisements/postadv" });

            ViewBag.Countries = new LocationsHandler().GetCountries().ToSelectItemList();
            ViewBag.Categories = new AdvertisementsHandler().GetCategories().ToSelectItemList();
           
            //types[0].Selected = true;
            //ViewBag.AdvTypes = types;
            return View();
        }

        [HttpPost]
        public ActionResult PostAdv(FormCollection data)
        {
            User cUser = (User)Session[WebUtil.CURRENT_USER];
            if (cUser == null) return RedirectToAction("Login", "Users", new { returnUrl = "advertisements/postadv" });

            //try
            //{
            if (Convert.ToBoolean(data["IAgree"].Split(',').First()))
            {
                Advertisement adv = new Advertisement();
                //adv.City = new City { Id = Convert.ToInt32(data["City"]) };
                adv.Title = data["AdvTitle"];
                adv.Price = Convert.ToSingle(data["Price"]);
               // adv.StartsOn = Convert.ToDateTime(data["StartsOn"]);
                //adv.EndsOn = Convert.ToDateTime(data["EndsOn"]);
               // adv.SubCategory = new SubCategory { Id = Convert.ToInt32(data["SubCategory"]) };
                //adv.Type = new AdvertisementType { Id = Convert.ToInt32(data["AdvType"]) };
                adv.Description = data["Description"];
                adv.IsFeatured = false;
                adv.Status = new AdvertisementStatus { Id = 1 };
                adv.AddedOn = DateTime.Now;
             
                int counter = 0;
                long uno = DateTime.Now.Ticks;
                foreach (string fcName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fcName];
                    if (!string.IsNullOrWhiteSpace(file.FileName))
                    {
                        string url = "/dataimages/products/" + $"{uno}_{++counter}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
                        string path = Request.MapPath(url);
                        file.SaveAs(path);
                        adv.Images.Add(new AdvertisementImage { Url = url});
                    }
                }
                new AdvertisementsHandler().Add(adv);
                TempData.Add("Message", new AlertModel("The advertisement added successfully", AlertType.Success));
            }
            //}
            //catch
            //{
            //    TempData.Add("Message", new AlertModel("Failed to add the advertisement", AlertType.Error));
            //}
            return RedirectToAction("PostAdv");
        }

        //[HttpGet]
        //public ActionResult SubCategories(int id)
        //{
        //    DDLViewModel m = new DDLViewModel();
        //    m.Name = "SubCategory";
        //    m.Caption = "- Sub Categories -";
        //   m.Values = new AdvertisementsHandler().GetSubCategories(new Category { Id = id }).ToSelectItemList();
        //    m.GlyphIcon = "glyphicon-tag";
        //    return PartialView("~/Views/Shared/_DDLView.cshtml", m);
        //}



    }
}



