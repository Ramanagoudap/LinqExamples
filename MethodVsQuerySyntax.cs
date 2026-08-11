using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class MethodVsQuerySyntax
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== METHOD SYNTAX vs QUERY SYNTAX ===\n");

        var products = SampleData.GetProducts();
        var categories = SampleData.GetCategories();

        // Q1: Filter with both syntaxes
        Console.WriteLine("Q1: Filter products with price > 100");

        // Method syntax
        Console.WriteLine("Method Syntax:");
        var methodFilter = products.Where(p => p.Price > 100).Select(p => p.Name);
        methodFilter.ToList().ForEach(n => Console.WriteLine($"  {n}"));

        // Query syntax
        Console.WriteLine("Query Syntax:");
        var queryFilter = from p in products
                          where p.Price > 100
                          select p.Name;
        queryFilter.ToList().ForEach(n => Console.WriteLine($"  {n}"));

        // Q2: OrderBy with both syntaxes
        Console.WriteLine("\nQ2: Sort by price descending");

        // Method syntax
        Console.WriteLine("Method Syntax:");
        var methodSort = products.OrderByDescending(p => p.Price).Select(p => $"{p.Name}: ${p.Price}");
        methodSort.ToList().ForEach(x => Console.WriteLine($"  {x}"));

        // Query syntax
        Console.WriteLine("Query Syntax:");
        var querySort = from p in products
                        orderby p.Price descending
                        select $"{p.Name}: ${p.Price}";
        querySort.ToList().ForEach(x => Console.WriteLine($"  {x}"));

        // Q3: GroupBy with both syntaxes
        Console.WriteLine("\nQ3: Group by category");

        // Method syntax
        Console.WriteLine("Method Syntax:");
        var methodGroup = products
            .GroupBy(p => p.CategoryId)
            .Select(g => new { CategoryId = g.Key, Count = g.Count() });
        methodGroup.ToList().ForEach(x => Console.WriteLine($"  Category {x.CategoryId}: {x.Count} items"));

        // Query syntax
        Console.WriteLine("Query Syntax:");
        var queryGroup = from p in products
                         group p by p.CategoryId into g
                         select new { CategoryId = g.Key, Count = g.Count() };
        queryGroup.ToList().ForEach(x => Console.WriteLine($"  Category {x.CategoryId}: {x.Count} items"));

        // Q4: Join with both syntaxes
        Console.WriteLine("\nQ4: Join products with categories");

        // Method syntax
        Console.WriteLine("Method Syntax:");
        var methodJoin = products
            .Join(categories,
                p => p.CategoryId,
                c => c.Id,
                (p, c) => $"{p.Name} ({c.Name})");
        methodJoin.ToList().ForEach(x => Console.WriteLine($"  {x}"));

        // Query syntax
        Console.WriteLine("Query Syntax:");
        var queryJoin = from p in products
                        join c in categories on p.CategoryId equals c.Id
                        select $"{p.Name} ({c.Name})";
        queryJoin.ToList().ForEach(x => Console.WriteLine($"  {x}"));

        // Q5: Complex query with both syntaxes
        Console.WriteLine("\nQ5: Filter, order, and select");

        // Method syntax
        Console.WriteLine("Method Syntax:");
        var methodComplex = products
            .Where(p => p.Stock > 10)
            .OrderBy(p => p.Price)
            .Select(p => new { p.Name, p.Price, p.Stock });
        methodComplex.ToList().ForEach(x => Console.WriteLine($"  {x.Name}: ${x.Price} ({x.Stock} stock)"));

        // Query syntax
        Console.WriteLine("Query Syntax:");
        var queryComplex = from p in products
                           where p.Stock > 10
                           orderby p.Price
                           select new { p.Name, p.Price, p.Stock };
        queryComplex.ToList().ForEach(x => Console.WriteLine($"  {x.Name}: ${x.Price} ({x.Stock} stock)"));

        // Q6: Multiple from clauses (SelectMany equivalent)
        Console.WriteLine("\nQ6: Multiple from clauses (cross join)");

        // Query syntax with multiple from
        var queryMultiFrom = from c in categories
                             from p in products
                             where p.CategoryId == c.Id
                             select $"{c.Name} > {p.Name}";
        queryMultiFrom.ToList().ForEach(x => Console.WriteLine($"  {x}"));

        // Equivalent method syntax
        var methodMultiFrom = categories
            .SelectMany(c => products.Where(p => p.CategoryId == c.Id),
                (c, p) => $"{c.Name} > {p.Name}");
        Console.WriteLine("(Method syntax equivalent above)");

        // Q7: Let clause (only in query syntax)
        Console.WriteLine("\nQ7: Let clause - introduce intermediate variable");
        var queryLet = from p in products
                       let discount = p.Price * 0.1m
                       where p.Price > 100
                       select new { p.Name, Original = p.Price, Discount = discount, Final = p.Price - discount };
        queryLet.ToList().ForEach(x => Console.WriteLine($"  {x.Name}: ${x.Original:F2} - ${x.Discount:F2} = ${x.Final:F2}"));

        // Equivalent in method syntax (use Select with intermediate calculation)
        var methodLet = products
            .Where(p => p.Price > 100)
            .Select(p => new
            {
                p.Name,
                Original = p.Price,
                Discount = p.Price * 0.1m,
                Final = p.Price - (p.Price * 0.1m)
            });
        Console.WriteLine("(Method syntax equivalent above)");

        // Q8: Group join (into clause)
        Console.WriteLine("\nQ8: Left join with group");
        var queryGroupJoin = from c in categories
                             join p in products on c.Id equals p.CategoryId into prods
                             select new { Category = c.Name, Products = prods };
        foreach (var item in queryGroupJoin)
        {
            Console.WriteLine($"  {item.Category}: {string.Join(", ", item.Products.Select(p => p.Name))}");
        }
    }
}


