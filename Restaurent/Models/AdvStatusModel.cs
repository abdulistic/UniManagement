using Restaurant.ClassLibrary.PakClassified;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class AdvStatusModel
    {
        public AdvStatusModel(AdvertisementStatus status)
        {
            name = status.Name;

            if (status.Id == 1)
            {
                cssClass = "label-info";
            }
            else
            {
                if (status.Id==2)
                {
                    cssClass = "label-success";
                }
                else
                {
                    cssClass = "label-danger";
                }
            }
        }

        private string name;
        public string Name { get { return name; } }

        private string cssClass;
        public string CssClass { get { return cssClass; } }
    }
}