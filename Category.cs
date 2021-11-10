using System.Collections;
using System.Collections.Generic;

namespace WorkingWithEFCore_Northwind
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private ICollection<Product> Products { get; set; }= new HashSet<Product>();
    }
}