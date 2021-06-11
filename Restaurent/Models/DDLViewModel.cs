using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurent.Models
{
    public class DDLViewModel
    {
        public string Name { get; set; }

        public string Caption { get; set; }

        public string GlyphIcon { get; set; }

        public IEnumerable<SelectListItem> Values { get; set; }



    }
}