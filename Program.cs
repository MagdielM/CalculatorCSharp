using System;

namespace CalculatorCSharp
{
    static class Program
    {
        static void Main(string[] args)
        {
            // TODO: Write tutorial messages.
            Console.WriteLine("Input expression:");
            Calculator.InitialRawExpression = Console.ReadLine();
        }
    }
}
