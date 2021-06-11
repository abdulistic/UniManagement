﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoClassLibrary.UsersMgt
{
    public class UserImage
    {
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(300)]
        public string Url { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Caption { get; set; }

        public int Priority { get; set; }
    }
}
