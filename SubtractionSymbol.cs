using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class SubtractionSymbol : Symbol, IResolvable
    {
        public SubtractionSymbol()
        {
            level = (int)OperationType.AddOrSubtract;
        }
        public NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber)
        {
            return leftNumber - rightNumber;
        }
    }
}
