using System;
using System.Collections.Generic;

namespace CalculatorCSharp
{
    public class NumberSymbol : Symbol
    {

        public NumberSymbol(string numericString)
        {
            NumericValue = double.Parse(numericString);
        }

        public double NumericValue { get; set; }
    }
}
