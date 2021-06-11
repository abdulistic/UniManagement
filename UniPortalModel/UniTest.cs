using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class UniTest
    {
        public int TestId { get; set; }
        public string TestName { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public int SubjectId { get; set; }
    }
}
