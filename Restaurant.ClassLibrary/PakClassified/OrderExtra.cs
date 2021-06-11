using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.PakClassified
{
    public class OrderExtra:IListable
    {
        public int Id { get; set; }
        public int ExtraId { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }

        public int ItemId { get; set; }
        //[ForeignKey("OrderDetail_Id")]
        public virtual OrderDetail OrderDetail { get; set; }
    }
}
