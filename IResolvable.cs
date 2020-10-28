using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CalculatorCSharp
{
    interface IResolvable
    {
        NumberSymbol Resolve(NumberSymbol leftNumber, NumberSymbol rightNumber);
    }
}
