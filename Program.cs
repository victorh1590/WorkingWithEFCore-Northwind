// See https://aka.ms/new-console-template for more information
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WorkingWithEFCore_Northwind;

static void QueryingCategories()
{
    using (var db = new Northwind())
    {
        Console.WriteLine("Categories and how many products they have:");

        IQueryable<Category> cats = db.Categories.Include(c => c.Products);

        foreach (Category c in cats)
        {
            Console.WriteLine($"{c.Name} has {c.Products.Count}.");
        }
    }
}

static void FilteredIncludes()
{
    using (var db = new Northwind())
    {
        Console.Write("Enter a minimum for units in stock: ");
        string unitsInStock = Console.ReadLine();
        int stock = int.Parse(unitsInStock);

        IQueryable<Category> categories = db.Categories
            .Include(c => c.Products
                .Where(p => p.Stock >= stock));

        Console.WriteLine($"ToQueryString: {categories.ToQueryString()}\n");
        
        foreach (var cat in categories)
        {
            Console.WriteLine($"{cat.Name} has {cat.Products.Count} products with a minimum of {stock} units in stock.");

            foreach (var prod in cat.Products)
            {
                Console.WriteLine($"    {prod.Name} has {prod.Stock} units in stock.");    
            }
        }
    }
}

static void QueryingProducts()
{
    using (var db = new Northwind())
    {
        string productsInRange;
        decimal price;
        do
        {
            Console.Write("Enter a price: ");
            productsInRange = Console.ReadLine();
        } while (!decimal.TryParse(productsInRange, out price));

        IQueryable<Product> prods = db.Products
            .Where(product => product.Cost > price)
            .OrderByDescending(product => product.Cost);

        foreach (var item in prods)
        {
            Console.WriteLine("{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
                item.Id, item.Name, item.Cost, item.Stock);
        }
    }
}

// QueryingCategories();
// FilteredIncludes();
QueryingProducts();