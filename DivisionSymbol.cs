using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class DivisionSymbol : Symbol, IResolvable
    {
        public DivisionSymbol()
        {
            level = (int)OperationType.MultiplyOrDivide;
        }
        public NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber)
        {
            return leftNumber / rightNumber;
        }
    }
}
