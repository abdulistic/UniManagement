using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class EditItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public bool IsFeatured { get; set; }
        public float Price { get; set; }

    }
}