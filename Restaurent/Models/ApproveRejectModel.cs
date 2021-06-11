using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurent.Models
{
    public class ApproveRejectModel
    {
        public int AdvertisementId { get; set; }

        public int RequiredAction { get; set; }
    }
}