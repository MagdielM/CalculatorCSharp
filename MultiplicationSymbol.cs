using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class MultiplicationSymbol : Symbol, IResolvable
    {
        public MultiplicationSymbol()
        {
            level = (int)OperationType.MultiplyOrDivide;
        }
        public NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber)
        {
            return leftNumber * rightNumber;
        }
    }
}
