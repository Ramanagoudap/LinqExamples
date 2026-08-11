# LINQ Examples for C# - Most Asked Questions

A comprehensive, interactive console project demonstrating the most commonly asked LINQ coding questions and patterns in C#.

## Project Structure

```
LinqExamples/
├── Program.cs                      # Interactive menu and entry point
├── Models.cs                       # Data models and sample data
├── FilteringAndProjection.cs       # Q1-Q10: Where, Select, Contains, etc.
├── OrderingAndGrouping.cs          # Q1-Q12: OrderBy, GroupBy, ThenBy, etc.
├── JoinsAndAggregation.cs          # Q1-Q12: Join, GroupJoin, Sum, Count, Average, etc.
├── SetOperationsAndPartitioning.cs # Q1-Q13: Take, Skip, Union, Intersect, Except, Chunk, etc.
├── AdvancedLinq.cs                 # Q1-Q20: SelectMany, Zip, Aggregate, ToLookup, etc.
├── MethodVsQuerySyntax.cs          # Comparison of method and query syntax
├── CommonPatterns.cs               # Q1-Q20: Real-world patterns and scenarios
├── PerformanceAndAsync.cs          # Q1-Q14: Performance tips, Async LINQ
├── LinqExamples.csproj             # Project file (.NET 8)
└── README.md                       # This file
```

## File Descriptions

### Program.cs
- Interactive menu system
- Easy navigation between topics
- Run individual examples or all at once

### Models.cs
Contains:
- **Product** - Id, Name, Price, CategoryId, Stock, CreatedDate
- **Category** - Id, Name
- **Order** - Id, CustomerId, Total, OrderDate
- **Customer** - Id, Name, Email
- **SampleData** - Static methods providing test data for all examples

### FilteringAndProjection.cs (10 Questions)
1. Filter products by price
2. Project to get names only
3. Filter low stock items
4. Filter and project combined
5. Multiple AND conditions
6. Multiple OR conditions
7. Filter using Contains
8. Filter with index
9. Exclude items (NOT)
10. Filter null/empty values

### OrderingAndGrouping.cs (12 Questions)
1. Sort ascending by price
2. Sort descending by price
3. Multi-column sort (OrderBy then ThenBy)
4. Group by single property
5. Group and count
6. Group and calculate aggregates
7. Group by multiple properties
8. First item from each group
9. Sort by date
10. Reverse order
11. Order by newest first
12. Sort within groups

### JoinsAndAggregation.cs (12 Questions)
1. Inner Join
2. Left Join (GroupJoin)
3. Count all items
4. Count with condition
5. Sum aggregation
6. Average calculation
7. Min and Max
8. Multiple aggregates at once
9. Aggregation by group
10. Join with aggregation
11. Complex join and aggregate
12. Any and All conditions

### SetOperationsAndPartitioning.cs (13 Questions)
1. Take first N items
2. Skip first N items
3. Pagination (Skip + Take)
4. TakeLast
5. SkipLast
6. TakeWhile
7. SkipWhile
8. Distinct/Remove duplicates
9. Union (combine lists)
10. Intersect (common items)
11. Except (difference)
12. Concat (with duplicates)
13. Chunk (group into N-sized chunks)

### AdvancedLinq.cs (20 Questions)
1. SelectMany (flatten nested collections)
2. First and FirstOrDefault
3. Last and LastOrDefault
4. Single and SingleOrDefault
5. Contains check
6. OfType filtering
7. Cast to type
8. DefaultIfEmpty
9. Range generation
10. Repeat values
11. Reverse
12. DistinctBy
13. Custom sort (multiple keys)
14. Zip sequences
15. Group by custom key/switch expression
16. Complex WHERE with multiple conditions
17. ToLookup (fast lookup table)
18. ToDictionary conversion
19. Aggregate (fold/reduce)
20. FindAll with predicate

### MethodVsQuerySyntax.cs
Demonstrates both syntaxes side-by-side:
1. Filter (Where)
2. OrderBy (descending)
3. GroupBy
4. Join
5. Complex query (Where, OrderBy, Select)
6. Multiple from clauses
7. Let clause (query syntax only)
8. Group join with into clause

### CommonPatterns.cs (20 Questions)
1. Top N items
2. Bottom N items
3. Pagination
4. Search/multi-field filter
5. Remove duplicates
6. Find missing IDs
7. Bulk operations
8. Check any/all conditions
9. Handle empty results
10. Conditional SELECT
11. Complex conditions (switch expressions)
12. Group and statistics
13. Hierarchical/nested grouping
14. Filter with index
15. Sum by condition
16. Date filtering
17. String operations
18. Case-insensitive search
19. Batch processing
20. Quick lookup (ToLookup)

### PerformanceAndAsync.cs (14 Questions)
1. Deferred execution (lazy)
2. Immediate execution (ToList)
3. ToArray
4. ToDictionary
5. Reusing queries
6. Multiple iterations on deferred query
7. Avoid multiple enumeration
8. Combine filters efficiently
9. Lazy vs eager evaluation timing
10. AsEnumerable() usage
11. GroupBy memory usage
12. Pagination performance
13. Filter before sorting
14. Async iteration

## How to Use

### Option 1: Interactive Menu
```bash
dotnet run
```
Use the menu to select different LINQ topics. Each topic contains multiple examples that run sequentially.

### Option 2: Direct Execution
```bash
dotnet run
```
Then select option "9" to run all examples at once.

### Sample Data
All examples use pre-defined sample data:
- 6 Products with various prices and stock levels
- 2 Categories
- 5 Orders
- 3 Customers

## Key Concepts Covered

✓ Filtering (Where, Contains, Any, All)
✓ Projection (Select, SelectMany)
✓ Sorting (OrderBy, OrderByDescending, ThenBy, Reverse)
✓ Grouping (GroupBy, GroupJoin)
✓ Joining (Join, GroupJoin)
✓ Aggregation (Sum, Count, Average, Min, Max)
✓ Set Operations (Union, Intersect, Except, Distinct)
✓ Partitioning (Take, Skip, TakeWhile, SkipWhile, Chunk)
✓ Conversion (ToList, ToArray, ToDictionary, ToLookup)
✓ Element Operations (First, Last, Single, ElementAt)
✓ Generation (Range, Repeat)
✓ Quantifiers (Any, All, Contains)
✓ Concatenation (Concat, Zip)
✓ Method vs Query Syntax
✓ Performance Considerations
✓ Async LINQ

## Prerequisites

- .NET 8.0 SDK or later
- C# 8.0 or later

## Running the Project

```bash
cd C:\Users\rapati\source\repos
dotnet restore
dotnet run
```

## Example Walkthrough

### Example 1: Simple Filter and Select
```csharp
var expensiveProducts = products
    .Where(p => p.Price > 100)
    .Select(p => p.Name);
```

### Example 2: Group and Aggregate
```csharp
var avgByCategory = products
    .GroupBy(p => p.CategoryId)
    .Select(g => new { 
        CategoryId = g.Key, 
        AvgPrice = g.Average(p => p.Price) 
    });
```

### Example 3: Join
```csharp
var productWithCategory = products
    .Join(categories,
        p => p.CategoryId,
        c => c.Id,
        (p, c) => new { p.Name, Category = c.Name });
```

## Tips for Learning

1. Start with `FilteringAndProjection` if new to LINQ
2. Experiment by modifying conditions in examples
3. Compare `MethodVsQuerySyntax` to understand both approaches
4. Study `CommonPatterns` for real-world usage
5. Review `PerformanceAndAsync` for optimization tips

## Common LINQ Mistakes to Avoid

- ❌ Enumerating the same IEnumerable multiple times without materializing
- ❌ Calling `.Count()` on large deferred queries when you only need to check if any exist
- ❌ Forgetting to materialize with `.ToList()` before modifying results
- ❌ Using `.OrderBy()` before `.Where()` (inefficient)
- ❌ Not using `.Any()` instead of `.Count() > 0`

## Performance Notes

- Use `.ToList()` when you'll iterate multiple times
- Use `.Where()` before `.OrderBy()` to reduce sorting overhead
- Use `.AsEnumerable()` to switch to LINQ-to-Objects from other providers
- Use `.ToLookup()` for fast multi-level lookups
- Consider `Chunk()` for batch processing large datasets

## Further Reading

- Microsoft LINQ Documentation: https://docs.microsoft.com/en-us/dotnet/csharp/linq/
- LINQ Standard Query Operators: https://docs.microsoft.com/en-us/dotnet/csharp/linq/standard-query-operators/

---

**Created**: 2026
**Framework**: .NET 8.0
**Language**: C# 12.0
