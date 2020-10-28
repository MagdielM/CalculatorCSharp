using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class PowerSymbol : Symbol, IResolvable
    {
        public PowerSymbol()
        {
            level = (int)OperationType.Power;
        }
        public NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber)
        {
            return leftNumber.Pow(rightNumber);
        }
    }
}
