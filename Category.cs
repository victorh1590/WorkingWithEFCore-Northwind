using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore_Northwind
{
    public class Category
    {
        [Key]
        [Column("CategoryID")]
        public int Id { get; set; }
        
        [Column("CategoryName")]
        public string Name { get; set; }
        
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = new HashSet<Product>();
        }
    }
}