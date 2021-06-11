using Restaurant.ClassLibrary;
using Restaurant.ClassLibrary.PakClassified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurent.Models
{
    public static class ModelsHelper
    {
        public static List<SelectListItem> ToSelectItemList(this IEnumerable<IListable> values)
        {
            List<SelectListItem> tempList = new List<SelectListItem>();
            foreach (var v in values)
            {
                tempList.Add(new SelectListItem { Text =v.Name, Value = Convert.ToString(v.Id) });
            }
            tempList.TrimExcess();
            return tempList;

        }

        public static List<FeaturedRecipesModel>  ToAdvSummaryModelList(this List<Advertisement> advertisements)
        {
            List<FeaturedRecipesModel> modelList = new List<FeaturedRecipesModel>();
            foreach (var adv in advertisements)
            {
                FeaturedRecipesModel m = new FeaturedRecipesModel();
                m.Id = adv.Id;
                m.Title = adv.Title;
                m.Price = adv.Price;
                m.Description = adv.Description;
                m.ImageUrl = (adv.Images.Count() > 0) ? adv.Images.First().Url : "/images/temp/nophoto.png";
                modelList.Add(m);
            }
            modelList.TrimExcess();
            return modelList;
        }

        public static List<ChefOrderModel> ToChefOrderModelList(this List<Order> orders)
        {
            List<ChefOrderModel> modelList = new List<ChefOrderModel>();
            foreach (var o in orders)
            {
                ChefOrderModel m = new ChefOrderModel();
                m.Id = o.Id;
                m.UserName = o.User.FullName;
                m.Price = o.Price;
                m.Time = o.AddedOn.ToLongTimeString();
                m.Date = o.AddedOn.ToLongDateString();
                //m.Status = o.Status;
                m.Address = o.User.FullName;
                foreach (var d in o.OrderDetail)
                {
                    m.ChefOrderItem.Add(new ChefOrderDetailModel { Id = d.Id, Title = d.Name });
                }
                modelList.Add(m);
            }
            modelList.TrimExcess();
            return modelList;
        }
    }
}