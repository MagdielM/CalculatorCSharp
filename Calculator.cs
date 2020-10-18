using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CalculatorCSharp
{
    static class Calculator
    {
        /// <summary>
        /// Base level for nested expressions, used to determine operation
        /// order. Set to 3 so nested expressions receive priority over all
        /// normal operations.
        /// </summary>
        private const int baseNestingLevel = 3;
        public const string defaultErrorMessageEnd = "Please modify your expression accordingly and try again.";

        /// <summary>
        /// Gets or sets the initial raw expression entered by the user.
        /// </summary>
        /// <remarks>
        /// The set value will be enclosed in parentheses before being assigned.
        /// When set, the Calculator will always attempt to parse the value of the property.
        /// </remarks>
        public static string InitialRawExpression
        {
            get { return InitialRawExpression; }
            set
            {
                InitialRawExpression = "(" + value + ")";
                DeepestNestingLevel = default;
                initialExpression = Parse(InitialRawExpression, out _, baseNestingLevel);
            }
        }


        private static Expression initialExpression;
        public static NumberSymbol result;
        public static string RawExpression { get; private set; }
        public static int DeepestNestingLevel { get; set; } = baseNestingLevel;

        private static Expression Parse(string rawExpression, out int offset, int level = baseNestingLevel)
        {
            Expression returnExpression = new Expression(level);

            /* symbolStringBuilder is to be used for all expression substrings, so the
             * initial length is quadrupled from default to ensure reasonably long 
             * expressions don't allocate repeatedly. */
            StringBuilder symbolStringBuilder = new StringBuilder(64);

            for (int i = 0; i < rawExpression.Length; i++)
            {
                // Append digits to sStringBuilder
                if (char.IsDigit(rawExpression[i]))
                {
                    symbolStringBuilder.Append(rawExpression[i]);

                }

                else if (char.IsPunctuation(rawExpression[i]))
                {
                    switch (rawExpression[i])
                    {
                        case '.':
                            if (!char.IsDigit(rawExpression[i + 1]))
                            {
                                throw new ArgumentException("Decimal points must be followed by a digit. " +
                                    defaultErrorMessageEnd);
                            }
                            else
                            {
                                if (!char.IsDigit(rawExpression[i - 1]))
                                {
                                    symbolStringBuilder.Append('0');
                                }
                                symbolStringBuilder.Append('.');
                            }
                            break;

                        case '(':
                            int charactersToSkip;
                            returnExpression.expression.Add(Parse(rawExpression.Substring(i + 1), out charactersToSkip, level + 1));
                            DeepestNestingLevel++;
                            i += charactersToSkip + 1;
                            break;

                        case ')':
                            if (char.IsDigit(rawExpression[i - 1]))
                            {
                                returnExpression.expression.Add(new NumberSymbol(rawExpression));
                            }
                            else
                            {
                                throw new ArgumentException("All expressions, nested or otherwise, must" +
                                    " end with a digit." + defaultErrorMessageEnd);
                            }
                            offset = i;
                            return returnExpression;

                        default:
                            throw new ArgumentException($"The symbol \'{rawExpression[i]}\' is invalid. " +
                                defaultErrorMessageEnd);
                    }
                }
                else if (char.IsSymbol(rawExpression[i]))
                {

                }
            }
            offset = default;
            return returnExpression;
        }

        private class Expression : Symbol
        {
            public int order;
            public List<Symbol> expression = new List<Symbol>();

            public Expression(int initialOrder, string initialRawExpression = "")
            {
                order = initialOrder;
                rawSymbol = initialRawExpression;
            }
        }
    }
}
