using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore_Northwind
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        
        public virtual ICollection<Product> Products { get; set; }= new HashSet<Product>();
    }
}