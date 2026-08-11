using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class FilteringAndProjection
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== FILTERING AND PROJECTION ===\n");

        var products = SampleData.GetProducts();

        // Q1: Filter products with price greater than 100
        Console.WriteLine("Q1: Products with price > 100");
        var expensiveProducts = products.Where(p => p.Price > 100);
        expensiveProducts.ToList().ForEach(p => Console.WriteLine(p));

        // Q2: Get product names only
        Console.WriteLine("\nQ2: Product names only");
        var productNames = products.Select(p => p.Name);
        productNames.ToList().ForEach(name => Console.WriteLine(name));

        // Q3: Get products with low stock (< 15)
        Console.WriteLine("\nQ3: Low stock products (< 15)");
        var lowStockProducts = products.Where(p => p.Stock < 15);
        lowStockProducts.ToList().ForEach(p => Console.WriteLine($"{p.Name}: {p.Stock} units"));

        // Q4: Filter and project - get name and price of expensive items
        Console.WriteLine("\nQ4: Name and price of products > 100");
        var result = products
            .Where(p => p.Price > 100)
            .Select(p => new { p.Name, p.Price });
        result.ToList().ForEach(x => Console.WriteLine($"{x.Name}: ${x.Price}"));

        // Q5: Filter with multiple conditions (AND)
        Console.WriteLine("\nQ5: Products in category 2 AND price > 30");
        var filtered = products.Where(p => p.CategoryId == 2 && p.Price > 30);
        filtered.ToList().ForEach(p => Console.WriteLine(p));

        // Q6: Filter with multiple conditions (OR)
        Console.WriteLine("\nQ6: Products with CategoryId 1 OR price > 200");
        var orFiltered = products.Where(p => p.CategoryId == 1 || p.Price > 200);
        orFiltered.ToList().ForEach(p => Console.WriteLine(p));

        // Q7: Filter using Contains (check if name contains substring)
        Console.WriteLine("\nQ7: Products containing 'er' in name");
        var nameContains = products.Where(p => p.Name.Contains("er", StringComparison.OrdinalIgnoreCase));
        nameContains.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q8: Filter and project with index (where clause with index)
        Console.WriteLine("\nQ8: Every second product");
        var everySecond = products.Where((p, index) => index % 2 == 0);
        everySecond.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q9: Exclude items based on condition (NOT)
        Console.WriteLine("\nQ9: Products excluding category 1");
        var excluded = products.Where(p => p.CategoryId != 1);
        excluded.ToList().ForEach(p => Console.WriteLine(p));

        // Q10: Filter null or empty
        Console.WriteLine("\nQ10: Filter null/empty check");
        var items = new List<string> { "Apple", null, "Banana", "", "Cherry" };
        var nonEmpty = items.Where(x => !string.IsNullOrEmpty(x));
        nonEmpty.ToList().ForEach(x => Console.WriteLine(x));
    }
}


