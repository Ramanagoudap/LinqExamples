using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqExamples;

class Program
{
    static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║        COMPREHENSIVE LINQ EXAMPLES FOR C# CODING       ║");
        Console.WriteLine("║           Most Asked Questions and Patterns           ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");

        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine()?.Trim() ?? "";

            Console.Clear();

            switch (choice)
            {
                case "1":
                    FilteringAndProjection.RunExamples();
                    break;
                case "2":
                    OrderingAndGrouping.RunExamples();
                    break;
                case "3":
                    JoinsAndAggregation.RunExamples();
                    break;
                case "4":
                    SetOperationsAndPartitioning.RunExamples();
                    break;
                case "5":
                    AdvancedLinq.RunExamples();
                    break;
                case "6":
                    MethodVsQuerySyntax.RunExamples();
                    break;
                case "7":
                    CommonPatterns.RunExamples();
                    break;
                case "8":
                    PerformanceAndAsync.RunExamples();
                    break;
                case "9":
                    RunAll();
                    break;
                case "0":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
            }

            Console.WriteLine("\n\nPress any key to return to menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\n╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    SELECT A TOPIC                      ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════╣");
        Console.WriteLine("║ 1. Filtering & Projection (Where, Select)             ║");
        Console.WriteLine("║ 2. Ordering & Grouping (OrderBy, GroupBy)             ║");
        Console.WriteLine("║ 3. Joins & Aggregation (Join, Sum, Count, Average)    ║");
        Console.WriteLine("║ 4. Set Operations & Partitioning (Union, Take, Skip)  ║");
        Console.WriteLine("║ 5. Advanced LINQ (SelectMany, Zip, Aggregate)         ║");
        Console.WriteLine("║ 6. Method vs Query Syntax                             ║");
        Console.WriteLine("║ 7. Common Patterns & Real-World Scenarios             ║");
        Console.WriteLine("║ 8. Performance & Async LINQ                           ║");
        Console.WriteLine("║ 9. Run All Examples                                   ║");
        Console.WriteLine("║ 0. Exit                                               ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");
        Console.Write("Enter your choice (0-9): ");
    }

    static void RunAll()
    {
        Console.WriteLine("Running all examples...\n");

        FilteringAndProjection.RunExamples();
        PressKeyToContinue();

        OrderingAndGrouping.RunExamples();
        PressKeyToContinue();

        JoinsAndAggregation.RunExamples();
        PressKeyToContinue();

        SetOperationsAndPartitioning.RunExamples();
        PressKeyToContinue();

        AdvancedLinq.RunExamples();
        PressKeyToContinue();

        MethodVsQuerySyntax.RunExamples();
        PressKeyToContinue();

        CommonPatterns.RunExamples();
        PressKeyToContinue();

        PerformanceAndAsync.RunExamples();
    }

    static void PressKeyToContinue()
    {
        Console.WriteLine("\n\nPress any key to continue to next section...");
        Console.ReadKey();
        Console.Clear();
    }
}
