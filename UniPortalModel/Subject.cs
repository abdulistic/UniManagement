using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public long? TeacherId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int? ClassId { get; set; }
    }
}
