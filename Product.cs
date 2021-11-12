using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorkingWithEFCore_Northwind
{
    public class Product
    {
        [Key]
        [Column("ProductID")]
        public int Id { get; set; }
        
        [Required]
        [StringLength(40)]
        [Column("ProductName")]
        public string Name { get; set; }
        
        [Column("UnitPrice", TypeName = "money")]
        public decimal? Cost { get; set; }
        
        [Column("UnitsInStock")]
        public short? Stock { get; set; }
        
        public bool Discontinued { get; set; }
        
        public int CategoryId { get; set; }
        
        public virtual Category Category { get; set; }
    }
}