using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class AdditionSymbol : Symbol, IResolvable
    {
        public AdditionSymbol()
        {
            level = (int)OperationType.AddOrSubtract;
        }
        public NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber)
        {
            return leftNumber + rightNumber;
        }
    }
}
