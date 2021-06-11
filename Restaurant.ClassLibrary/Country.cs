using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.ClassLibrary
{
    //[Table("MyTable")]
    public class Country : IListable
    {
        public int Id { get; set; }


        public Nullable<int> Code { get; set; }
            
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

    }

    public class Users : IListable
    {
        public int Id { get; set; }


        public Nullable<int> Code { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

    }
}
