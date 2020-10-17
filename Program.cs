using System;

namespace CalculatorCSharp
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input expression:");
            Calculator.InitialRawExpression = Console.ReadLine();
        }
    }
}
