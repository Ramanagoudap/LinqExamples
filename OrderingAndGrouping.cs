using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class OrderingAndGrouping
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== ORDERING AND GROUPING ===\n");

        var products = SampleData.GetProducts();
        var orders = SampleData.GetOrders();

        // Q1: Sort products by price (ascending)
        Console.WriteLine("Q1: Products sorted by price (ascending)");
        var sortedAsc = products.OrderBy(p => p.Price);
        sortedAsc.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q2: Sort products by price (descending)
        Console.WriteLine("\nQ2: Products sorted by price (descending)");
        var sortedDesc = products.OrderByDescending(p => p.Price);
        sortedDesc.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q3: Sort by multiple columns
        Console.WriteLine("\nQ3: Sort by CategoryId then by Price");
        var multiSort = products
            .OrderBy(p => p.CategoryId)
            .ThenByDescending(p => p.Price);
        multiSort.ToList().ForEach(p => Console.WriteLine($"Cat: {p.CategoryId}, {p.Name}: ${p.Price}"));

        // Q4: Group products by category
        Console.WriteLine("\nQ4: Group products by category");
        var groupedByCategory = products.GroupBy(p => p.CategoryId);
        foreach (var group in groupedByCategory)
        {
            Console.WriteLine($"Category {group.Key}:");
            group.ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));
        }

        // Q5: Group and aggregate - category with product count
        Console.WriteLine("\nQ5: Product count by category");
        var groupCounts = products
            .GroupBy(p => p.CategoryId)
            .Select(g => new { CategoryId = g.Key, Count = g.Count() });
        groupCounts.ToList().ForEach(x => Console.WriteLine($"Category {x.CategoryId}: {x.Count} products"));

        // Q6: Group and get aggregates
        Console.WriteLine("\nQ6: Average price by category");
        var avgByCategory = products
            .GroupBy(p => p.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                AvgPrice = g.Average(p => p.Price),
                Count = g.Count()
            });
        avgByCategory.ToList().ForEach(x => Console.WriteLine($"Category {x.CategoryId}: Avg ${x.AvgPrice:F2}, Count: {x.Count}"));

        // Q7: Group by multiple properties
        Console.WriteLine("\nQ7: Group by category and price range");
        var groupMultiple = products
            .GroupBy(p => new { p.CategoryId, PriceRange = p.Price > 100 ? "Expensive" : "Cheap" });
        foreach (var group in groupMultiple)
        {
            Console.WriteLine($"Category {group.Key.CategoryId}, {group.Key.PriceRange}: {group.Count()} items");
        }

        // Q8: Get first item from each group
        Console.WriteLine("\nQ8: First (cheapest) item from each category");
        var firstInGroup = products
            .GroupBy(p => p.CategoryId)
            .Select(g => g.OrderBy(p => p.Price).First());
        firstInGroup.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q9: Order by creation date
        Console.WriteLine("\nQ9: Products ordered by creation date");
        var orderByDate = products.OrderBy(p => p.CreatedDate);
        orderByDate.ToList().ForEach(p => Console.WriteLine($"{p.Name}: {p.CreatedDate:yyyy-MM-dd}"));

        // Q10: Reverse order
        Console.WriteLine("\nQ10: Products in reverse (descending by price)");
        var reversed = products.OrderByDescending(p => p.Price).Reverse();
        reversed.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q11: Stable sort with reversed results
        Console.WriteLine("\nQ11: Orders by date (newest first)");
        var ordersDesc = orders.OrderByDescending(o => o.OrderDate);
        ordersDesc.ToList().ForEach(o => Console.WriteLine($"Order {o.Id}: {o.OrderDate:yyyy-MM-dd}, Total: {o.Total:C}"));

        // Q12: Group and sort within groups
        Console.WriteLine("\nQ12: Categories with sorted products");
        var sortedGroups = products
            .GroupBy(p => p.CategoryId)
            .OrderBy(g => g.Key);
        foreach (var group in sortedGroups)
        {
            Console.WriteLine($"Category {group.Key}:");
            group.OrderBy(p => p.Name).ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));
        }
    }
}


