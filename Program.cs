using System;

namespace CalculatorCSharp
{
    static class Program
    {
        static void Main(string[] args)
        {
            // TODO: Write tutorial messages.
            // TODO: Print exceptions to console and place input prompt in loop.

            Console.WriteLine("Input expression: ");
            while (true)
            {
                try
                {
                    Calculator.InitialRawExpression = Console.ReadLine();
                    Console.WriteLine($"Result: {Calculator.Resolve()}\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Enter another expression or press Esc to quit.\n");
                }
            }

        }
    }
}
