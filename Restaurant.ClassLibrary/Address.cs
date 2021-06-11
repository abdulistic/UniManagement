using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string StreetAddress { get; set; }


        [Required]
        public virtual City City { get; set; }

    }
}
