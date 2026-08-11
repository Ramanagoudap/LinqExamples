using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class AdvancedLinq
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== ADVANCED LINQ ===\n");

        var products = SampleData.GetProducts();
        var categories = SampleData.GetCategories();

        // Q1: SelectMany - flatten nested collections
        Console.WriteLine("Q1: SelectMany - flatten categories with products");
        var categoryProducts = categories
            .Join(products,
                c => c.Id,
                p => p.CategoryId,
                (c, p) => new { Category = c, Product = p })
            .SelectMany(x => new[] { x })
            .GroupBy(x => x.Category.Name)
            .Select(g => new { Category = g.Key, Products = g.Select(x => x.Product.Name).ToList() });

        foreach (var cat in categoryProducts)
        {
            Console.WriteLine($"{cat.Category}: {string.Join(", ", cat.Products)}");
        }

        // Q2: First, FirstOrDefault
        Console.WriteLine("\nQ2: First product and FirstOrDefault");
        var first = products.First();
        var firstOrDefault = products.FirstOrDefault();
        Console.WriteLine($"First: {first.Name}");
        Console.WriteLine($"FirstOrDefault: {firstOrDefault?.Name}");

        // Q3: Last, LastOrDefault
        Console.WriteLine("\nQ3: Last product");
        var last = products.Last();
        Console.WriteLine($"Last: {last.Name}");

        // Q4: Single - expects exactly one match
        Console.WriteLine("\nQ4: Single item with condition");
        var singleProduct = products.Where(p => p.Id == 5).SingleOrDefault();
        Console.WriteLine($"Product with id 5: {singleProduct?.Name}");

        // Q5: Contains
        Console.WriteLine("\nQ5: Check if list contains item");
        var productIds = new List<int> { 1, 2, 3 };
        var containsId3 = productIds.Contains(3);
        Console.WriteLine($"List contains 3: {containsId3}");

        // Q6: OfType - filter by type
        Console.WriteLine("\nQ6: OfType filtering");
        var objects = new List<object> { "string", 1, 2.5, "another", 3 };
        var strings = objects.OfType<string>();
        Console.WriteLine("Strings only:");
        strings.ToList().ForEach(s => Console.WriteLine($"  {s}"));

        // Q7: Cast - convert to specific type
        Console.WriteLine("\nQ7: Cast to specific type");
        var intList = new List<int> { 1, 2, 3, 4, 5 };
        var objectList = intList.Cast<object>();
        Console.WriteLine($"Cast count: {objectList.Count()}");

        // Q8: Default if empty
        Console.WriteLine("\nQ8: DefaultIfEmpty");
        var empty = products.Where(p => p.Id > 1000).DefaultIfEmpty(products.First());
        empty.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q9: Range - generate sequences
        Console.WriteLine("\nQ9: Range - generate numbers 1-5");
        var range = Enumerable.Range(1, 5);
        range.ToList().ForEach(n => Console.Write($"{n} "));
        Console.WriteLine();

        // Q10: Repeat - generate repeated values
        Console.WriteLine("\nQ10: Repeat - generate 'X' 3 times");
        var repeated = Enumerable.Repeat("X", 3);
        repeated.ToList().ForEach(x => Console.Write($"{x} "));
        Console.WriteLine();

        // Q11: Reverse
        Console.WriteLine("\nQ11: Reverse order");
        var reversed = products.ToList().AsEnumerable().Reverse();
        reversed.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q12: Distinct with custom comparer
        Console.WriteLine("\nQ12: Distinct products by category");
        var distinctByCategory = products.DistinctBy(p => p.CategoryId);
        distinctByCategory.ToList().ForEach(p => Console.WriteLine($"{p.Name} (Category {p.CategoryId})"));

        // Q13: OrderBy with custom logic
        Console.WriteLine("\nQ13: Products by name length, then price");
        var customSort = products
            .OrderBy(p => p.Name.Length)
            .ThenBy(p => p.Price);
        customSort.ToList().ForEach(p => Console.WriteLine($"{p.Name} ({p.Name.Length} chars): ${p.Price}"));

        // Q14: Zip - combine two sequences
        Console.WriteLine("\nQ14: Zip two sequences");
        var names = new List<string> { "Product A", "Product B", "Product C" };
        var prices = new List<decimal> { 100, 200, 300 };
        var zipped = names.Zip(prices, (name, price) => $"{name}: ${price}");
        zipped.ToList().ForEach(z => Console.WriteLine(z));

        // Q15: GroupBy with custom key
        Console.WriteLine("\nQ15: Group by price range");
        var priceGroups = products
            .GroupBy(p => p.Price switch
            {
                < 50 => "Cheap",
                < 200 => "Medium",
                _ => "Expensive"
            });
        foreach (var group in priceGroups)
        {
            Console.WriteLine($"{group.Key}: {string.Join(", ", group.Select(p => p.Name))}");
        }

        // Q16: Complex where with multiple conditions
        Console.WriteLine("\nQ16: Complex filtering");
        var complex = products
            .Where(p => p.Stock > 5)
            .Where(p => p.Price > 50)
            .Where(p => p.Name.Length > 4);
        complex.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q17: ToLookup for fast multi-level lookup
        Console.WriteLine("\nQ17: ToLookup - fast lookup by category");
        var lookup = products.ToLookup(p => p.CategoryId);
        Console.WriteLine($"Products in category 1: {string.Join(", ", lookup[1].Select(p => p.Name))}");

        // Q18: Dictionary conversion
        Console.WriteLine("\nQ18: Convert to Dictionary");
        var productDict = products.ToDictionary(p => p.Id, p => p.Name);
        Console.WriteLine($"Product with id 2: {productDict[2]}");

        // Q19: Aggregate (fold/reduce)
        Console.WriteLine("\nQ19: Aggregate - concatenate names");
        var concatenated = products
            .Select(p => p.Name)
            .Aggregate((acc, name) => acc + ", " + name);
        Console.WriteLine($"All products: {concatenated}");

        // Q20: FindAll with predicate
        Console.WriteLine("\nQ20: Find products matching condition");
        var priceRange = products.Where(p => p.Price >= 100 && p.Price <= 300).ToList();
        priceRange.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));
    }
}


