using Restaurent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.ClassLibrary;
using Restaurent.Service;
using Restaurant.ClassLibrary.ViewModel;
using System.Threading.Tasks;

namespace Restaurent.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAdminService service;

        public UsersController()
        {
            this.service = new AdminService();
        }

        [HttpGet]
        public async Task<ActionResult> Login()
        {
            string temp = Request.QueryString["returnURL"];
            ViewBag.ReturnURL = temp;
            HttpCookie cookie = Request.Cookies[WebUtil.LoginCookie];

            if (cookie != null)
            {
                string[] data = cookie.Value.Split(',');
                cookie.Expires = DateTime.Now.AddDays(3);
                Response.SetCookie(cookie);
                UserVM userVM = new UserVM() { UserName = data[0], Password = data[1] };


                UserVM user = await service.GetUser(userVM);
                if (!string.IsNullOrEmpty(user?.UserName))
                {
                    Session.Add(WebUtil.CurrentUser, user);
                    if (!string.IsNullOrWhiteSpace(temp))
                    {
                        string[] parts = temp.Split('/');
                        return RedirectToAction(parts[1], parts[0]);
                    }
                    if (user.Role.Equals(WebUtil.Admin))
                    {
                        return RedirectToAction("usermanagement", "admin");
                    }
                    else if (user.Role.Equals(WebUtil.Teacher))
                    {
                        return RedirectToAction("teachermanagement", "teacher");
                    }
                    else if (user.Role.Equals(WebUtil.Student))
                    {
                        return RedirectToAction("subjectslist", "student");
                    }
                    else
                    {
                        return RedirectToAction("login", "users");
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            UserVM userVM = new UserVM() { UserName = model.UserName, Password = model.Password };


            UserVM response = await service.GetUser(userVM);
            if (!string.IsNullOrEmpty(response?.UserName))
            {
                Session.Add(WebUtil.CurrentUser, response);
                if (Request.Browser.Cookies)
                {
                    if (model.RememberMe)
                    {
                        HttpCookie loginCookie = new HttpCookie(WebUtil.LoginCookie);
                        loginCookie.Expires = DateTime.Now.AddDays(3);
                        loginCookie.Value = $"{model.UserName},{model.Password}";
                        Response.SetCookie(loginCookie);
                    }
                }
                string temp = Request.QueryString["rURL"];
                if (!string.IsNullOrWhiteSpace(temp))
                {
                    string[] parts = temp.Split('/');
                    return RedirectToAction(parts[1], parts[0]);
                }
                if (response.Role.Equals(WebUtil.Admin))
                {
                    return RedirectToAction("usermanagement", "admin");
                }
                else if (response.Role.Equals(WebUtil.Teacher))
                {
                    return RedirectToAction("teachermanagement", "teacher");
                }
                else if (response.Role.Equals(WebUtil.Student))
                {
                    return RedirectToAction("subjectslist", "student");
                }
                else
                {
                    return RedirectToAction("login", "users");
                }
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
            return RedirectToAction("login");
        }

    }
}