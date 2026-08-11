using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class CommonPatterns
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== COMMON PATTERNS AND SCENARIOS ===\n");

        var products = SampleData.GetProducts();
        var categories = SampleData.GetCategories();
        var orders = SampleData.GetOrders();

        // Q1: Top N items (Top 3 most expensive)
        Console.WriteLine("Q1: Top 3 most expensive products");
        var topN = products.OrderByDescending(p => p.Price).Take(3);
        topN.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q2: Bottom N items (Bottom 3 cheapest)
        Console.WriteLine("\nQ2: Bottom 3 cheapest products");
        var bottomN = products.OrderBy(p => p.Price).Take(3);
        bottomN.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q3: Pagination
        Console.WriteLine("\nQ3: Pagination (page 1, size 2)");
        var page = GetPage(products, 1, 2);
        page.ToList().ForEach(p => Console.WriteLine($"{p.Name}"));

        // Q4: Search/Filter multiple fields
        Console.WriteLine("\nQ4: Search products by name or category");
        var searchTerm = "m";
        var searchResults = products.Where(p =>
            p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            p.Price.ToString().Contains(searchTerm));
        searchResults.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q5: Remove duplicates
        Console.WriteLine("\nQ5: Distinct products by price");
        var distinctPrices = products.DistinctBy(p => p.Price);
        distinctPrices.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q6: Find missing IDs in sequence
        Console.WriteLine("\nQ6: Find missing product IDs");
        var allIds = Enumerable.Range(1, products.Max(p => p.Id)).ToHashSet();
        var presentIds = products.Select(p => p.Id).ToHashSet();
        var missingIds = allIds.Except(presentIds);
        Console.WriteLine($"Missing IDs: {string.Join(", ", missingIds)}");

        // Q7: Bulk operations
        Console.WriteLine("\nQ7: Update multiple items (bulk operation)");
        var expensiveProducts = products.Where(p => p.Price > 200).ToList();
        foreach (var product in expensiveProducts)
        {
            Console.WriteLine($"Would reduce price of {product.Name} from ${product.Price}");
        }

        // Q8: Check if any/all conditions
        Console.WriteLine("\nQ8: Conditional checks");
        var hasLowStock = products.Any(p => p.Stock < 10);
        var allPositivePrice = products.All(p => p.Price > 0);
        Console.WriteLine($"Has low stock: {hasLowStock}");
        Console.WriteLine($"All positive prices: {allPositivePrice}");

        // Q9: Default values for empty results
        Console.WriteLine("\nQ9: Handle empty results safely");
        var expensive = products.Where(p => p.Price > 5000).FirstOrDefault();
        Console.WriteLine($"Most expensive (>5000): {expensive?.Name ?? "None found"}");

        // Q10: Conditional SELECT
        Console.WriteLine("\nQ10: Conditional projection");
        var priceStatus = products.Select(p => new
        {
            p.Name,
            Status = p.Price > 500 ? "Expensive" : "Affordable"
        });
        priceStatus.ToList().ForEach(x => Console.WriteLine($"{x.Name}: {x.Status}"));

        // Q11: Case when style (Switch expression)
        Console.WriteLine("\nQ11: Complex conditional selection");
        var productStatus = products.Select(p => new
        {
            p.Name,
            Status = p.Stock switch
            {
                0 => "Out of stock",
                < 10 => "Low stock",
                < 30 => "Medium stock",
                _ => "Good stock"
            }
        });
        productStatus.ToList().ForEach(x => Console.WriteLine($"{x.Name}: {x.Status}"));

        // Q12: Group and get statistics
        Console.WriteLine("\nQ12: Statistics by category");
        var stats = products
            .GroupBy(p => p.CategoryId)
            .Select(g => new
            {
                Category = g.Key,
                Count = g.Count(),
                TotalStock = g.Sum(p => p.Stock),
                AvgPrice = g.Average(p => p.Price),
                MaxPrice = g.Max(p => p.Price)
            });
        stats.ToList().ForEach(x => Console.WriteLine(
            $"Cat {x.Category}: {x.Count} items, Total stock: {x.TotalStock}, Avg: ${x.AvgPrice:F2}, Max: ${x.MaxPrice:F2}"));

        // Q13: Hierarchical/nested grouping
        Console.WriteLine("\nQ13: Nested grouping");
        var hierarchical = products
            .GroupBy(p => p.CategoryId)
            .Select(cg => new
            {
                Category = cg.Key,
                PriceRanges = cg
                    .GroupBy(p => p.Price > 100 ? "Expensive" : "Cheap")
                    .Select(pr => new { Range = pr.Key, Count = pr.Count() })
            });
        foreach (var cat in hierarchical)
        {
            Console.WriteLine($"Category {cat.Category}:");
            foreach (var range in cat.PriceRanges)
            {
                Console.WriteLine($"  {range.Range}: {range.Count} items");
            }
        }

        // Q14: Filter with index
        Console.WriteLine("\nQ14: Every other item");
        var everyOther = products.Where((p, i) => i % 2 == 0);
        everyOther.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q15: Sum by condition
        Console.WriteLine("\nQ15: Total value of expensive items");
        var totalExpensive = products
            .Where(p => p.Price > 200)
            .Sum(p => p.Price * p.Stock);
        Console.WriteLine($"Total value (>$200): ${totalExpensive:F2}");

        // Q16: Date filtering
        Console.WriteLine("\nQ16: Products created in last 30 days");
        var recent = products.Where(p => p.CreatedDate > DateTime.Now.AddDays(-30));
        recent.ToList().ForEach(p => Console.WriteLine($"{p.Name}: {p.CreatedDate:yyyy-MM-dd}"));

        // Q17: String operations
        Console.WriteLine("\nQ17: Products starting with specific letter");
        var startsWithL = products.Where(p => p.Name.StartsWith("L"));
        Console.WriteLine($"Starting with 'L': {string.Join(", ", startsWithL.Select(p => p.Name))}");

        // Q18: Case-insensitive comparison
        Console.WriteLine("\nQ18: Case-insensitive search");
        var caseInsensitive = products.FirstOrDefault(p =>
            p.Name.Equals("laptop", StringComparison.OrdinalIgnoreCase));
        Console.WriteLine($"Found: {caseInsensitive?.Name}");

        // Q19: Batch processing
        Console.WriteLine("\nQ19: Process in batches");
        var batchSize = 2;
        var batches = products.Chunk(batchSize);
        var batchNum = 1;
        foreach (var batch in batches)
        {
            Console.WriteLine($"Batch {batchNum}: {string.Join(", ", batch.Select(p => p.Name))}");
            batchNum++;
        }

        // Q20: Lookup for fast access
        Console.WriteLine("\nQ20: Quick lookup");
        var categoryLookup = products.ToLookup(p => p.CategoryId);
        Console.WriteLine("Category 2 products:");
        categoryLookup[2].ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));
    }

    private static List<T> GetPage<T>(IEnumerable<T> source, int pageNumber, int pageSize)
    {
        return source
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
}


