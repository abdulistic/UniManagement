using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class FeaturedRecipesModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public AdvStatusModel Status { get; set; }

    }
}