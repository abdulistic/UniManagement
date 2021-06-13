using System;
using System.Collections.Generic;

#nullable disable

namespace UniPortalModel
{
    public partial class TestResult
    {
        public int ResultId { get; set; }
        public long StudentId { get; set; }
        public int TestId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public string StudentMarks { get; set; }
    }
}
