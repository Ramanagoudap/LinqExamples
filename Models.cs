using System;
using System.Collections.Generic;

namespace LinqExamples;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public int Stock { get; set; }
    public DateTime CreatedDate { get; set; }

    public override string ToString() => $"Id: {Id}, Name: {Name}, Price: {Price}, CategoryId: {CategoryId}";
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString() => $"Id: {Id}, Name: {Name}";
}

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public decimal Total { get; set; }
    public DateTime OrderDate { get; set; }

    public override string ToString() => $"OrderId: {Id}, CustomerId: {CustomerId}, Total: {Total:C}";
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public override string ToString() => $"Id: {Id}, Name: {Name}, Email: {Email}";
}

public static class SampleData
{
    public static List<Product> GetProducts() => new()
    {
        new() { Id = 1, Name = "Laptop", Price = 1200, CategoryId = 1, Stock = 5, CreatedDate = new DateTime(2024, 1, 15) },
        new() { Id = 2, Name = "Mouse", Price = 25, CategoryId = 2, Stock = 50, CreatedDate = new DateTime(2024, 1, 20) },
        new() { Id = 3, Name = "Keyboard", Price = 75, CategoryId = 2, Stock = 30, CreatedDate = new DateTime(2024, 2, 10) },
        new() { Id = 4, Name = "Monitor", Price = 350, CategoryId = 1, Stock = 10, CreatedDate = new DateTime(2024, 2, 15) },
        new() { Id = 5, Name = "Headphones", Price = 150, CategoryId = 2, Stock = 20, CreatedDate = new DateTime(2024, 3, 1) },
        new() { Id = 6, Name = "Printer", Price = 200, CategoryId = 1, Stock = 8, CreatedDate = new DateTime(2024, 3, 10) }
    };

    public static List<Category> GetCategories() => new()
    {
        new() { Id = 1, Name = "Electronics" },
        new() { Id = 2, Name = "Accessories" }
    };

    public static List<Order> GetOrders() => new()
    {
        new() { Id = 1, CustomerId = 1, Total = 1500, OrderDate = new DateTime(2024, 1, 20) },
        new() { Id = 2, CustomerId = 2, Total = 500, OrderDate = new DateTime(2024, 1, 25) },
        new() { Id = 3, CustomerId = 1, Total = 800, OrderDate = new DateTime(2024, 2, 10) },
        new() { Id = 4, CustomerId = 3, Total = 1200, OrderDate = new DateTime(2024, 2, 15) },
        new() { Id = 5, CustomerId = 2, Total = 300, OrderDate = new DateTime(2024, 3, 1) }
    };

    public static List<Customer> GetCustomers() => new()
    {
        new() { Id = 1, Name = "John Doe", Email = "john@example.com" },
        new() { Id = 2, Name = "Jane Smith", Email = "jane@example.com" },
        new() { Id = 3, Name = "Bob Johnson", Email = "bob@example.com" }
    };
}
