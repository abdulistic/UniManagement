using Restaurent.Models;
using Restaurant.ClassLibrary.UsersMgt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoClassLibrary.UsersMgt;
using Restaurant.ClassLibrary;

namespace Restaurent.Controllers
{
    public class UsersController : Controller
    {
        private static int city4Signup;

        [HttpGet]
        public ActionResult Login()
        {
            string temp = Request.QueryString["returnURL"];
            ViewBag.ReturnURL = temp;
            HttpCookie cookie = Request.Cookies[WebUtil.LoginCookie];
            try
            {

            }
            catch (Exception)
            {

                throw;
            }

            if (cookie != null)
            {
                string[] data = cookie.Value.Split(',');
                cookie.Expires = DateTime.Now.AddDays(3);
                Response.SetCookie(cookie);
                User user = new UsersHandler().GetUser(data[0], data[1]);
                if (user != null)
                {
                    Session.Add(WebUtil.CurrentUser, user);
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        string[] parts = temp.Split('/');
                        return RedirectToAction(parts[1], parts[0]);
                    }

                    if (user.IsInRole(WebUtil.AdminRole))
                    {
                        return RedirectToAction("admin", "home");
                    }
                    if (user.IsInRole(WebUtil.CHEF_ROLE))
                    {
                        return RedirectToAction("orders", "products");
                    }
                    return RedirectToAction("home", "home");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            User user = new UsersHandler().GetUser(model.UserName, model.Password);
            if (user != null)
            {
                Session.Add(WebUtil.CurrentUser, user);
                if (Request.Browser.Cookies)
                {
                    if (model.RememberMe)
                    {
                        HttpCookie loginCookie = new HttpCookie(WebUtil.LoginCookie);
                        loginCookie.Expires = DateTime.Now.AddDays(3);
                        loginCookie.Value = $"{user.LoginId},{user.Password}";
                        Response.SetCookie(loginCookie);
                    }
                }
                string temp = Request.QueryString["rURL"];
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    string[] parts = temp.Split('/');
                    return RedirectToAction(parts[1], parts[0]);
                }
                if (user.IsInRole(WebUtil.AdminRole))
                {
                    return RedirectToAction("admin", "home");
                }
                if (user.IsInRole(WebUtil.CHEF_ROLE))
                {
                    return RedirectToAction("orders", "products");
                }
                return RedirectToAction("home", "home");
            }

            int attempts = Convert.ToInt32(Request["LoginAttempts"]);
            if (++attempts > 2)
            {
                return RedirectToAction("RecoverPassword");
            }
            ViewBag.LoginAttempts = attempts;
            return View();
        }

        [HttpGet]
        public ActionResult logout()
        {
            Session.Abandon();
            HttpCookie temp = Request.Cookies[WebUtil.LoginCookie];
            if (temp != null)
            {
                temp.Expires = DateTime.Now;
                Response.SetCookie(temp);
            }
            return RedirectToAction("home", "home");
        }

        [HttpGet]
        public ActionResult Signup()
        {
            ViewBag.Gender = new UsersHandler().GetGender().ToSelectItemList();
            

            return View();
        }


        [HttpPost]
        public ActionResult SignUp(SignUpModel data)
        {

            User user = new User();
            user.LoginId = data.LoginId;
            user.FullName = data.FullName;

            user.Email = data.Email;
            user.Password = data.Password;
            user.ConfirmPassword = data.Password;
            user.Address = data.Address;
            user.BirthDate = Convert.ToDateTime(data.BirthDate);
            user.ContactNumber = data.ContactNumber;
            user.Gender = new UserGender { Id = Convert.ToInt32(data.Gender) };
            if (city4Signup>0)
            {
            user.City = new City {Id = city4Signup};
                city4Signup = 0;
            }
            user.Role = new Role { Id = 3 };
            int counter = 0;
            long uno = DateTime.Now.Ticks;
            user.Image = new List<UserImage>();
            HttpPostedFileBase file = Request.Files[0];
            string url = "/dataimages/products/" + $"{uno}_{++counter}{file.FileName.Substring(file.FileName.LastIndexOf('.'))}";
            string path = Request.MapPath(url);
            file.SaveAs(path);
           // user.ImageUrl = url;
            user.Image.Add(new UserImage { Url = url, Priority = counter });

            new UsersHandler().Add(user);


            User oUser = new UsersHandler().GetUser(data.LoginId, data.Password);
            if (oUser != null)
            {
                Session.Add(WebUtil.CurrentUser, oUser);

                string temp = Request.QueryString["rURL"];
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    string[] parts = temp.Split('/');
                    return RedirectToAction(parts[1], parts[0]);
                }
                if (oUser.IsInRole(WebUtil.AdminRole))
                {
                    return RedirectToAction("admin", "home");
                }
                if (oUser.IsInRole(WebUtil.CHEF_ROLE))
                {
                    return RedirectToAction("chef", "home");
                }
                return RedirectToAction("home", "home");
            }



            return RedirectToAction("home", "home");
        }



       
        [HttpGet]
        public ActionResult CutomerDetails(int id)
        {
            User user = new UsersHandler().GetUserWithId(id);
            City city = new LocationsHandler().GetCityById(user.City.Id);
            CustomerDetailsModel customer = new CustomerDetailsModel() { Id = user.Id, UserName = user.FullName, Mobile = user.ContactNumber, Address = user.Address, Gender= user.Gender.Name, City = city.Name, State = city.Province.Name, Country = city.Province.Country.Name };

            return PartialView("~/Views/Shared/_UserDetailsPartialView.cshtml", customer);
        }

        [HttpGet]
        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditUserProfile(FormCollection form)
        {
            User cUser = (User)Session[WebUtil.CurrentUser];
            if (!(cUser != null)) return RedirectToAction("Login", "Users", new { returnUrl = "users/userprofile" });
            User user = new UsersHandler().GetUserWithId(Convert.ToInt32(form["Id"]));
            if (!(string.IsNullOrEmpty(form["Name"])))
            {
                user.FullName= form["Name"];
            }

            if (!(string.IsNullOrEmpty(form["Username"])))
            {
                user.LoginId = form["Username"];
            }

            if (!(string.IsNullOrEmpty(form["Email"])))
            {
                user.Email = form["Email"];
            }

            if (!(string.IsNullOrEmpty(form["Contact"])))
            {
                user.ContactNumber = form["Contact"];

            }

            if (!(string.IsNullOrEmpty(form["Address"])))
            {
                user.Address = form["Address"];

            }

            new UsersHandler().EditUser(user);
            return RedirectToAction("UserProfile");
        }

        public void GetUserCity(int id)
        {
            city4Signup = id;
        }

        [HttpGet]
        public ActionResult UserProfile()
        {
            User cUser = (User)Session[WebUtil.CurrentUser];
            if (!(cUser != null)) return RedirectToAction("Login", "Users", new { returnUrl = "users/userprofile" });

            User user = new UsersHandler().GetUserWithId(cUser.Id);
            CustomerDetailsModel model = new CustomerDetailsModel();

            model.Id = user.Id;
            model.Name = user.FullName;
            model.UserName = user.LoginId;
            model.Mobile = user.ContactNumber;
            model.ImageUrl = user.Image.First().Url;
            model.Country = user.City.Province.Country.Name;
            model.State = user.City.Province.Name;
            model.City = user.City.Name;
            model.Address = user.Address;
            model.Email = user.Email;
            model.Gender = user.Gender.Name;
            model.DOB = user.BirthDate.Value.ToShortDateString();


            return View(model);
        }
    }
}