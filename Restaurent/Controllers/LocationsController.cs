using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurent.Models;
using Restaurant.ClassLibrary;

namespace Restaurent.Controllers
{
    public class LocationsController : Controller
    {
        //select cities by country id
        [HttpGet]
        public ActionResult Provinces(int id)
        {
            //DDLViewModel m = new DDLViewModel();
            //m.Name = "Province";
            //m.Caption = "- Provinces -";
            ViewBag.Provinces = new LocationsHandler().GetProvinces(new Country { Id = id }).ToSelectItemList();
            //m.GlyphIcon = "glyphicon-map-marker";
            return PartialView("~/Views/Shared/_ProvincesCityPartialView.cshtml", ViewBag.Provinces);
        }

        [HttpGet]
        public ActionResult Cities(int id)
        {
            //DDLViewModel m = new DDLViewModel();
            //m.Name = "City";
            //m.Caption = "- Cities -";
            ViewBag.Cities = new LocationsHandler().GetCities(new Province { Id = id }).ToSelectItemList();
            //m.GlyphIcon = "glyphicon-map-marker";
            return PartialView("~/Views/Shared/_CityPartialView.cshtml", ViewBag.Cities);
        }
    }
}