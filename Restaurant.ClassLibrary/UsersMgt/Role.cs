using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.ClassLibrary.UsersMgt
{
    public class Role
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string name;

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        
        
    }
}
