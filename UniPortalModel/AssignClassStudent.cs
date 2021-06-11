using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class AssignClassStudent
    {
        public int AssignClassId { get; set; }
        public long StudentId { get; set; }
        public int? ClassId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
