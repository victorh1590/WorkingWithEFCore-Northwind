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

QueryingCategories();

