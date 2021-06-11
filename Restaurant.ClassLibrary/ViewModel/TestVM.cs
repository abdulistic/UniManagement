﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.ViewModel
{
    public class TestVM
    {
        public int TestId { get; set; }

        private string testName;

        [Required]
        [Display(Name = "Test Name")]
        public string TestName
        {
            get { return testName; }
            set { testName = value; }
        }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
