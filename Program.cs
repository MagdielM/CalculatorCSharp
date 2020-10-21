using System;

namespace CalculatorCSharp
{
    static class Program
    {
        static void Main(string[] args)
        {
            // TODO: Write tutorial messages.
            // TODO: Print exceptions to console and place input prompt in loop.
            Console.WriteLine("Input expression:");
            Calculator.InitialRawExpression = Console.ReadLine();
            Console.WriteLine($"Result: {Calculator.Resolve()}");
            Console.ReadLine();
        }
    }
}
