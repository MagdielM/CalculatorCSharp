using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    static class Calculator
    {
        public static string InitialRawExpression
        {
            get { return InitialRawExpression; }
            set
            {
                InitialRawExpression = value;
                Parse(value, initialExpression);
            }
        }
        /// <summary>
        /// Base level for nested expressions, used to determine operation
        /// order. Set to 3 so it receives priority over all normal operations.
        /// </summary>
        private const int baseLevel = 3;

        private static Expression initialExpression;
        public static string RawExpression { get; private set; }

        private static Symbol Parse(string rawExpression, Expression expresion, int level = baseLevel)
        {
            StringBuilder partialString = new StringBuilder(256);

            List<string> rawExpressionSymbols = new List<string>();
            for (int i = 0; i < rawExpression.Length; i++)
            {
                if (rawExpression[i] == '(')
                {
                    Expression nestedExpression = new Expression(level + 1);
                }
            }
        }

        private class Expression : Symbol
        {
            public int order;
            public string rawExpression;
            public List<Symbol> expression = new List<Symbol>();

            public Expression(int initialOrder)
            {
                order = initialOrder;
            }

            public Symbol Parse(int level)
            {
                Calculator.Parse(rawExpression, this, order);
            }
        }
    }
}
