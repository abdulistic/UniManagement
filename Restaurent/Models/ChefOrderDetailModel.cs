using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class ChefOrderDetailModel
    {
        //private ChefDetailExtrasModel chefDetailExtrasModel;

        public ChefOrderDetailModel()
        {
            ChefDetailExtras = new List<ChefDetailExtrasModel>();
        }

        //public ChefOrderDetailModel(ChefDetailExtrasModel chefDetailExtrasModel)
        //{
        //    this.chefDetailExtrasModel = chefDetailExtrasModel;
        //}

        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string Extras { get; set; }
        public virtual ICollection<ChefDetailExtrasModel> ChefDetailExtras { get; set; }
    }
}