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

        IQueryable<Category> cats = db.Categories;
            // .Include(c => c.Products);
            
        db.ChangeTracker.LazyLoadingEnabled = false;

        foreach (Category c in cats)
        {
            Console.WriteLine($"Explicitly load products for {c.Name}? (Y/N): ");
            ConsoleKeyInfo key = Console.ReadKey();
            Console.WriteLine();
            
            if (key.Key == ConsoleKey.Y)
            {
                var products = db.Entry(c).Collection(c => c.Products);
                if(!products.IsLoaded) products.Load();
            }

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

static void QueryingWithLike()
{
    using (var db = new Northwind())
    {
        Console.Write("Enter part of the product name: ");
        string input = Console.ReadLine();

        IQueryable<Product> products = db.Products
            .Where(p => EF.Functions.Like(p.Name, $"%{input}%"));

        foreach (var item in products)
        {
            Console.WriteLine("{0} has {1} units in stock. Discontinued? {2}",
                item.Name, item.Stock, item.Discontinued);
        }
    }
}

static bool AddProduct(int categoryId, string name, decimal? price)
{
    using var db = new Northwind();
    var newProduct = new Product
    {
        CategoryId = categoryId,
        Name = name,
        Cost = price
    };
    db.Products.Add(newProduct);
    return db.SaveChanges() == 1;
}

static void ListProducts()
{
    using var db = new Northwind();
    
    Console.WriteLine("{0,-3} {1,-35} {2,8} {3,5} {4}", "ID", "Product Name", "Cost", "Stock", "Disc.");

    foreach (var item in db.Products.OrderByDescending(p => p.Cost))
    {
        Console.WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
            item.Id, item.Name, item.Cost, item.Stock, item.Discontinued);
    }
}

// QueryingCategories();
// FilteredIncludes();
// QueryingProducts();
// QueryingWithLike();

// if (AddProduct(6, "Bob's Burgers II", 500M)) Console.WriteLine("Add product successful.");
ListProducts();