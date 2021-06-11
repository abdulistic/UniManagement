using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.ClassLibrary.PakClassified
{
    public class Category : IListable
    {
        public Category()
        {
            RecipeExtras = new List<Extras>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Name { get; set; }

        public virtual ICollection<Extras> RecipeExtras { get; set; }

       
    }
}
