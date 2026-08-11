using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinqExamples;

public class PerformanceAndAsync
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== PERFORMANCE AND ASYNC ===\n");

        var products = SampleData.GetProducts();

        // Q1: Deferred vs immediate execution
        Console.WriteLine("Q1: Deferred execution (lazy)");
        var deferredQuery = products.Where(p => p.Price > 100);
        Console.WriteLine("Query created but not executed yet");
        Console.WriteLine("Executing query:");
        deferredQuery.ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));

        // Q2: Immediate execution with ToList
        Console.WriteLine("\nQ2: Immediate execution with ToList()");
        var immediateList = products.Where(p => p.Price > 100).ToList();
        Console.WriteLine($"Created list with {immediateList.Count} items immediately");
        immediateList.ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));

        // Q3: ToArray for array
        Console.WriteLine("\nQ3: ToArray");
        var array = products.Where(p => p.Stock > 10).ToArray();
        Console.WriteLine($"Array has {array.Length} items");

        // Q4: ToDictionary for lookup table
        Console.WriteLine("\nQ4: ToDictionary for O(1) lookup");
        var productDict = products.ToDictionary(p => p.Id);
        Console.WriteLine($"Dictionary lookup for id 2: {productDict[2].Name}");

        // Q5: Reusing LINQ queries
        Console.WriteLine("\nQ5: Reuse vs re-query");
        var cheapProducts = products.Where(p => p.Price < 100).ToList();
        Console.WriteLine($"Reused list (materialized): {cheapProducts.Count} items");
        var count1 = cheapProducts.Count();
        var count2 = cheapProducts.Count();
        Console.WriteLine("Count called twice on materialized list (fast)");

        // Q6: Multiple iterations on deferred query (inefficient)
        Console.WriteLine("\nQ6: Multiple iterations on deferred query (inefficient)");
        var deferredExpensive = products.Where(p => p.Price > 100);
        var count3 = deferredExpensive.Count();
        var count4 = deferredExpensive.Count();
        Console.WriteLine("Count called twice on deferred query (inefficient - filters run twice)");

        // Q7: Avoid multiple enumeration issues
        Console.WriteLine("\nQ7: Avoid multiple enumeration");
        var inStockProducts = products.Where(p => p.Stock > 0).ToList(); // Materialize once
        foreach (var p in inStockProducts)
        {
            Console.WriteLine($"  {p.Name}");
        }

        // Q8: Combine filters before materializing
        Console.WriteLine("\nQ8: Combine filters efficiently");
        var filtered = products
            .Where(p => p.Price > 100)
            .Where(p => p.Stock > 5)
            .Where(p => p.CategoryId == 1)
            .ToList(); // Materialize once at end
        filtered.ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));

        // Q9: Lazy vs eager evaluation timing
        Console.WriteLine("\nQ9: Lazy evaluation timing");
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var lazyQuery = products.Where(p => ExpensiveFilter(p));
        sw.Stop();
        Console.WriteLine($"Query creation: {sw.ElapsedMilliseconds}ms (no filtering yet)");

        sw.Restart();
        var lazyList = lazyQuery.ToList();
        sw.Stop();
        Console.WriteLine($"Materialization: {sw.ElapsedMilliseconds}ms (filtering happens now)");

        // Q10: Using .AsEnumerable() for LINQ-to-Objects
        Console.WriteLine("\nQ10: AsEnumerable() for client-side filtering");
        var linqObjects = products.AsEnumerable().Where(p => p.Name.Length > 5);
        linqObjects.ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));

        // Q11: GroupBy allocation
        Console.WriteLine("\nQ11: GroupBy memory usage");
        var grouped = products.GroupBy(p => p.CategoryId);
        Console.WriteLine($"Created {grouped.Count()} groups");
        foreach (var group in grouped)
        {
            Console.WriteLine($"  Category {group.Key}: {group.Count()} items");
        }

        // Q12: Skip and Take for pagination performance
        Console.WriteLine("\nQ12: Pagination with Skip/Take");
        var pageSize = 2;
        var page1 = products.Skip(0).Take(pageSize).ToList();
        var page2 = products.Skip(pageSize).Take(pageSize).ToList();
        Console.WriteLine($"Page 1: {string.Join(", ", page1.Select(p => p.Name))}");
        Console.WriteLine($"Page 2: {string.Join(", ", page2.Select(p => p.Name))}");

        // Q13: Use Where before OrderBy
        Console.WriteLine("\nQ13: Filter before sorting (better performance)");
        var efficient = products
            .Where(p => p.Stock > 0)
            .OrderBy(p => p.Price)
            .ToList();
        Console.WriteLine($"Filtered and sorted: {efficient.Count} items");

        // Q14: Async iteration (demonstration)
        Console.WriteLine("\nQ14: Async LINQ demonstration");
        AsyncExample().GetAwaiter().GetResult();
    }

    private static bool ExpensiveFilter(Product p)
    {
        System.Threading.Thread.Sleep(1); // Simulate expensive operation
        return p.Price > 100;
    }

    private static async Task AsyncExample()
    {
        var products = SampleData.GetProducts();

        Console.WriteLine("Async iteration:");
        await Task.Delay(10); // Simulate async work

        foreach (var product in products)
        {
            if (product.Price > 100)
                Console.WriteLine($"  {product.Name}: ${product.Price}");
        }
    }
}


