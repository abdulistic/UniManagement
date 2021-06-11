using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class UniClass
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsActive { get; set; }
    }
}
