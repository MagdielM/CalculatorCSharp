using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class InversionSymbol : Symbol, IResolvable
    {
        public InversionSymbol()
        {
            level = (int)OperationType.Invert;
        }
        public NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber)
        {
            return -rightNumber;
        }
    }
}
