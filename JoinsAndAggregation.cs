using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

public class JoinsAndAggregation
{
    public static void RunExamples()
    {
        Console.WriteLine("\n=== JOINS AND AGGREGATION ===\n");

        var products = SampleData.GetProducts();
        var categories = SampleData.GetCategories();
        var orders = SampleData.GetOrders();
        var customers = SampleData.GetCustomers();

        // Q1: Inner Join
        Console.WriteLine("Q1: Join Products with Categories (Inner Join)");
        var innerJoin = products
            .Join(categories,
                p => p.CategoryId,
                c => c.Id,
                (p, c) => new { p.Name, p.Price, CategoryName = c.Name });
        innerJoin.ToList().ForEach(x => Console.WriteLine($"{x.Name} ({x.CategoryName}): ${x.Price}"));

        // Q2: Left Join using GroupJoin
        Console.WriteLine("\nQ2: Left Join - All categories with their products");
        var leftJoin = categories
            .GroupJoin(products,
                c => c.Id,
                p => p.CategoryId,
                (c, prods) => new { Category = c.Name, Products = prods });
        foreach (var group in leftJoin)
        {
            Console.WriteLine($"{group.Category}:");
            group.Products.ToList().ForEach(p => Console.WriteLine($"  {p.Name}"));
        }

        // Q3: Count of items
        Console.WriteLine("\nQ3: Total number of products");
        var totalCount = products.Count();
        Console.WriteLine($"Total products: {totalCount}");

        // Q4: Count with condition
        Console.WriteLine("\nQ4: Count products with stock > 15");
        var countWithCondition = products.Count(p => p.Stock > 15);
        Console.WriteLine($"Products with stock > 15: {countWithCondition}");

        // Q5: Sum aggregation
        Console.WriteLine("\nQ5: Total stock value");
        var totalValue = products.Sum(p => p.Price * p.Stock);
        Console.WriteLine($"Total inventory value: ${totalValue:F2}");

        // Q6: Average price
        Console.WriteLine("\nQ6: Average product price");
        var avgPrice = products.Average(p => p.Price);
        Console.WriteLine($"Average price: ${avgPrice:F2}");

        // Q7: Min and Max
        Console.WriteLine("\nQ7: Min and Max price");
        var minPrice = products.Min(p => p.Price);
        var maxPrice = products.Max(p => p.Price);
        Console.WriteLine($"Min price: ${minPrice}, Max price: ${maxPrice}");

        // Q8: Multiple aggregations at once
        Console.WriteLine("\nQ8: Multiple aggregates in one query");
        var stats = products
            .GroupBy(_ => 1)
            .Select(g => new
            {
                Total = g.Sum(p => p.Price),
                Average = g.Average(p => p.Price),
                Min = g.Min(p => p.Price),
                Max = g.Max(p => p.Price),
                Count = g.Count()
            })
            .First();
        Console.WriteLine($"Total: ${stats.Total:F2}, Avg: ${stats.Average:F2}, Min: ${stats.Min:F2}, Max: ${stats.Max:F2}, Count: {stats.Count}");

        // Q9: Aggregation by group
        Console.WriteLine("\nQ9: Total stock by category");
        var stockByCategory = products
            .GroupBy(p => p.CategoryId)
            .Select(g => new
            {
                CategoryId = g.Key,
                TotalStock = g.Sum(p => p.Stock),
                AvgPrice = g.Average(p => p.Price)
            });
        stockByCategory.ToList().ForEach(x => Console.WriteLine($"Category {x.CategoryId}: Stock={x.TotalStock}, AvgPrice=${x.AvgPrice:F2}"));

        // Q10: Join with aggregation
        Console.WriteLine("\nQ10: Orders by customer");
        var ordersByCustomer = customers
            .GroupJoin(orders,
                c => c.Id,
                o => o.CustomerId,
                (c, cOrders) => new
                {
                    Customer = c.Name,
                    OrderCount = cOrders.Count(),
                    TotalSpent = cOrders.Sum(o => o.Total)
                });
        ordersByCustomer.ToList().ForEach(x => Console.WriteLine($"{x.Customer}: {x.OrderCount} orders, Total: ${x.TotalSpent:C}"));

        // Q11: Complex join and aggregate
        Console.WriteLine("\nQ11: Product sales performance");
        var productSales = products
            .Join(orders,
                p => p.Id,
                o => o.Id,
                (p, o) => new { Product = p.Name, Order = o })
            .GroupBy(x => x.Product)
            .Select(g => new { Product = g.Key, TotalRevenue = g.Sum(x => x.Order.Total) });
        productSales.ToList().ForEach(x => Console.WriteLine($"{x.Product}: ${x.TotalRevenue:C}"));

        // Q12: Any and All
        Console.WriteLine("\nQ12: Any and All conditions");
        var hasExpensive = products.Any(p => p.Price > 1000);
        var allInStock = products.All(p => p.Stock > 0);
        Console.WriteLine($"Has product > $1000: {hasExpensive}");
        Console.WriteLine($"All products in stock: {allInStock}");
    }
}


