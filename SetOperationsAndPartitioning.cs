using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class SetOperationsAndPartitioning
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== SET OPERATIONS AND PARTITIONING ===\n");

        var products = SampleData.GetProducts();
        var categories = SampleData.GetCategories();

        // Q1: Take - get first N items
        Console.WriteLine("Q1: Get first 3 products");
        var first3 = products.Take(3);
        first3.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q2: Skip - skip first N items
        Console.WriteLine("\nQ2: Skip first 2 products and get rest");
        var afterSkip = products.Skip(2);
        afterSkip.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q3: Take and Skip together (pagination)
        Console.WriteLine("\nQ3: Pagination - Page 2, 2 items per page");
        var pageSize = 2;
        var pageNumber = 2;
        var page = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        page.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q4: TakeLast
        Console.WriteLine("\nQ4: Get last 2 products");
        var last2 = products.TakeLast(2);
        last2.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q5: SkipLast
        Console.WriteLine("\nQ5: Skip last 2 products");
        var skipLast = products.SkipLast(2);
        skipLast.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q6: TakeWhile
        Console.WriteLine("\nQ6: Take while price < 200");
        var takeWhile = products.OrderBy(p => p.Price).TakeWhile(p => p.Price < 200);
        takeWhile.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q7: SkipWhile
        Console.WriteLine("\nQ7: Skip while price < 100, then take rest");
        var skipWhile = products.OrderBy(p => p.Price).SkipWhile(p => p.Price < 100);
        skipWhile.ToList().ForEach(p => Console.WriteLine($"{p.Name}: ${p.Price}"));

        // Q8: Distinct - remove duplicates
        Console.WriteLine("\nQ8: Distinct categories");
        var numbers = new List<int> { 1, 2, 2, 3, 3, 3, 4 };
        var distinct = numbers.Distinct();
        distinct.ToList().ForEach(n => Console.Write($"{n} "));
        Console.WriteLine();

        // Q9: Union - combine two lists
        Console.WriteLine("\nQ9: Union of two product lists");
        var moreProducts = new List<Product>
        {
            new() { Id = 7, Name = "Cable", Price = 15, CategoryId = 2, Stock = 100, CreatedDate = DateTime.Now },
            new() { Id = 1, Name = "Laptop", Price = 1200, CategoryId = 1, Stock = 5, CreatedDate = DateTime.Now }
        };
        var union = products.Union(moreProducts, new ProductComparer());
        union.ToList().ForEach(p => Console.WriteLine(p.Name));

        // Q10: Intersect - common items
        Console.WriteLine("\nQ10: Intersect of two lists");
        var list1 = new List<int> { 1, 2, 3, 4, 5 };
        var list2 = new List<int> { 3, 4, 5, 6, 7 };
        var intersect = list1.Intersect(list2);
        intersect.ToList().ForEach(n => Console.Write($"{n} "));
        Console.WriteLine();

        // Q11: Except - items in first list but not in second
        Console.WriteLine("\nQ11: Except - unique to first list");
        var except = list1.Except(list2);
        except.ToList().ForEach(n => Console.Write($"{n} "));
        Console.WriteLine();

        // Q12: Concat - combine lists (with duplicates)
        Console.WriteLine("\nQ12: Concat two lists (keeps duplicates)");
        var concat = list1.Concat(list2);
        concat.ToList().ForEach(n => Console.Write($"{n} "));
        Console.WriteLine();

        // Q13: Chunk (break into groups of N)
        Console.WriteLine("\nQ13: Chunk products into groups of 2");
        var chunks = products.Chunk(2);
        foreach (var chunk in chunks)
        {
            Console.WriteLine($"Chunk: {string.Join(", ", chunk.Select(p => p.Name))}");
        }
    }
}

public class ProductComparer : IEqualityComparer<Product>
{
    public bool Equals(Product? x, Product? y) => x?.Id == y?.Id;
    public int GetHashCode(Product obj) => obj.Id.GetHashCode();
}


