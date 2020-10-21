﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorCSharp
{
    static class Calculator
    {
        /**
         * <summary>
         *   Base level for nested expressions, used to determine operation order. Set to 3
         *   so nested expressions receive priority over all normal operations.
         * </summary>
         */
        private const int baseNestingLevel = 3;
        public const string defaultErrorMessageEnd = "Please modify your expression accordingly and try again.";

        /**
         * <summary>
         *   Gets or sets the initial raw expression entered by the user.
         * </summary>
         *
         * <remarks>
         *   The set value will be stripped of whitespace before being assigned. The
         *   Calculator will attempt to parse the value of the property every time
         *   it is set.
         * </remarks>
         */
        public static string InitialRawExpression
        {
            get { return initialRawExpression; }
            set
            {
                initialRawExpression = string.Concat(value.Where(c => !char.IsWhiteSpace(c)));
                initialExpression = Parse(InitialRawExpression, out _, baseNestingLevel);
            }
        }
        private static string initialRawExpression;

        /**
         * <summary>
         *   Static array of parseable digits to be used with <see cref="Parse"/> as a
         *   substitute for <see cref="char.IsDigit"/>, as it can return true for
         *   characters outside of the parseable range.
         * </summary>
         */
        private static char[] ParseableDigits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /**
         * <summary>
         *   Static array of parseable operators to be used with <see cref="Parse"/>.
         * </summary>
         */
        private static char[] ParseableOperators = { '+', '-', '*', '/', '^' };

        /**
         * <summary>
         *   Static array of parseable expression delimiters to be used with
         *   <see cref="Parse"/>.
         * </summary>
         */
        private static char[] ParseableDelimiters = { '(', ')' };

        private static Expression initialExpression;
        public static NumberSymbol result;
        public static string RawExpression { get; private set; }
        public static int DeepestNestingLevel { get; set; } = baseNestingLevel;

        /**
         * <summary>
         *   Parses the input string into an <see cref="Expression"/> object, including
         *   nested expressions marked by parentheses.
         * </summary>
         *
         * <remarks>
         *   This method uses <see cref="StringBuilder"/> objects to perform repeated
         *   string operations during parsing. Particularly, <see cref="StringBuilder"/>
         *   objects are used buffer the individual strings to be passed into each
         *   <see cref="Symbol"/> parsed from <paramref name="rawExpression"/>.
         * </remarks>
         *
         * <param name="rawExpression">
         *   The string to be parsed as an <see cref="Expression"/>.
         * </param>
         *
         * <param name="offset">
         *   Out parameter used internally to offset the iteration variable by the amount of
         *   characters parsed by recursive calls to <see cref="Parse"/>. Should be passed a
         *   discard variable when called elsewhere.
         * </param>
         *
         * <param name="level">
         *   Parameter used internally to set the order field of nested
         *   <see cref="Expression"/>s generated by recursive calls to <see cref="Parse"/>,
         *   set to <see cref="baseNestingLevel"/> by default. Passing this parameter a
         *   different explicit value may result in undesired behavior.
         * </param>
         *
         * <returns>
         *   An <see cref="Expression"/> object with the <see cref="Symbol"/>s and nested
         *   expressions contained in <paramref name="rawExpression"/>.
         * </returns>
         * 
         * <exception cref="ArgumentException">
         *   Thrown when any invalid input is entered. Invalid input encompasses the
         *   following:
         *   <list type="bullet">
         *     <item>
         *       <description>
         *         Two or more consecutive operators, except the minus operator.
         *       </description>
         *     </item>
         *   </list>
         * </exception>
         */
        private static Expression Parse(string rawExpression, out int offset, int level = baseNestingLevel)
        {
            // rawExpression is nul-padded before parsing, so look-ahead and look-behind
            // conditionals don't go out of range.
            rawExpression = '\0' + rawExpression + '\0';
            Expression returnExpression = new Expression(level);

            // symbolStringBuilder is used for all expression substrings, so the initial
            // length is eight times the default to ensure reasonably long expressions
            // aren't fragmented (chunky), making iterations faster.
            StringBuilder symbolStringBuffer = new StringBuilder(128);

            for (int i = 0; i < rawExpression.Length; i++)
            {
                // Digits take precedence when parsing as most expressions are likely
                // to contain more numbers than anything else.
                if (ParseableDigits.Contains(rawExpression[i]))
                {
                    symbolStringBuffer.Append(rawExpression[i]);

                    // Parse symbolStringBuffer contents as new NumberSymbol if the
                    // following character is neither a period nor another digit.
                    if (!ParseableDigits.Contains(rawExpression[i + 1]) && rawExpression[i + 1] != '.')
                    {
                        returnExpression.Add(new NumberSymbol(symbolStringBuffer.ToString()));
                        symbolStringBuffer.Clear();
                    }
                }

                else if (rawExpression[i] == '.')
                {
                    // Ensure symbolStringBuffer does not contain multiple
                    // decimal points.
                    if (CheckForInvalidCharacter('.'))
                    {
                        throw new ArgumentException("One of the numbers in your expression contains " +
                            "multiple decimal points. " + defaultErrorMessageEnd);
                    }
                    if (!ParseableDigits.Contains(rawExpression[i - 1]))
                    {
                        // Assume non-prefixed decimal point indicates start of number
                        // between 0 and 1.
                        symbolStringBuffer.Append('0');
                    }
                    symbolStringBuffer.Append('.');
                }

                else if (ParseableOperators.Contains(rawExpression[i]))
                {
                    // Assume minus operators at the start of the sequence are additive
                    // inversion operators.
                    if (i == 1 && rawExpression[i] == '-')
                    {
                        returnExpression.Add(new OperationSymbol(OperationType.Invert, "-"));
                    }

                    else if (
                        // Check that the characters to either side are either digits,
                        // expression delimiters, or minus operators.
                        (ParseableDigits.Contains(rawExpression[i - 1])
                            && (rawExpression[i + 1] == '-'
                                || rawExpression[i + 1] == '(')
                        || ((ParseableDigits.Contains(rawExpression[i + 1])
                            && (rawExpression[i - 1] == '-'
                                || rawExpression[i - 1] == ')')))
                        || (rawExpression[i - 1] == ')' && rawExpression[i + 1] == '(')
                        || (ParseableDigits.Contains(rawExpression[i - 1])
                            && (ParseableDigits.Contains(rawExpression[i + 1])

                        // Check that the characters to the sides aren't both minus operators.
                        && (rawExpression[i - 1] != '-' && rawExpression[i + 1] != '-')))))

                    {
                        switch (rawExpression[i])
                        {
                            case '+':
                                returnExpression.Add(new OperationSymbol(OperationType.Add, "+"));
                                symbolStringBuffer.Clear();
                                break;

                            case '-':

                                // The operator is an additive inversion operator if it has a
                                // number to its right and no number to its left.
                                if (ParseableDigits.Contains(rawExpression[i + 1]))
                                {
                                    returnExpression.Add(new OperationSymbol(OperationType.Invert, "-"));
                                }

                                // The operator is a subtraction operator if it has a number
                                // to its left and either an additive inversion operator or
                                // another number to its right.
                                else
                                {
                                    returnExpression.Add(new OperationSymbol(OperationType.Subtract, "-"));
                                }
                                symbolStringBuffer.Clear();
                                break;

                            case '*':
                                returnExpression.Add(new OperationSymbol(OperationType.Multiply, "*"));
                                symbolStringBuffer.Clear();
                                break;

                            case '/':
                                returnExpression.Add(new OperationSymbol(OperationType.Divide, "*"));
                                symbolStringBuffer.Clear();
                                break;

                            case '^':
                                returnExpression.Add(new OperationSymbol(OperationType.Power, "^"));
                                symbolStringBuffer.Clear();
                                break;
                        }
                    }
                    else
                    {
                        throw new ArgumentException("The placement of one of your \"" +
                            $"{rawExpression[i]}\" operators is invalid. " + defaultErrorMessageEnd);

                    }
                }

                else if (ParseableDelimiters.Contains(rawExpression[i]))
                {
                    switch (rawExpression[i])
                    {
                        case '(':
                            int charactersToSkip;
                            returnExpression.InternalExpression.Add(
                                Parse(rawExpression.Substring(i + 1), out charactersToSkip, level + 1));
                            DeepestNestingLevel++;
                            i += charactersToSkip;
                            break;

                        case ')':
                            // At the end of an expression, consider all non-digit
                            // characters invalid.
                            if (!ParseableDigits.Contains(rawExpression[i - 1]))
                            {
                                throw new ArgumentException("All expressions, nested or otherwise, must" +
                                " end with a digit. " + defaultErrorMessageEnd);
                            }
                            offset = i;
                            returnExpression.rawSymbol = rawExpression.Substring(0, i);
                            return returnExpression;

                        default:
                            throw new ArgumentException($"The symbol \'{rawExpression[i]}\' is invalid. " +
                                defaultErrorMessageEnd);
                    }
                }
            }
            offset = default;
            returnExpression.rawSymbol = rawExpression[1..^1];
            return returnExpression;

            bool CheckForInvalidCharacter(char invalidChar)
            {
                foreach (char c in symbolStringBuffer.ToString())
                {
                    if (c == invalidChar)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private class Expression : Symbol
        {
            public int order;

            public double value;

            public List<Symbol> InternalExpression { get; private set; } = new List<Symbol>();

            public Expression(int initialOrder, string initialRawExpression = "")
            {
                order = initialOrder;
                rawSymbol = initialRawExpression;
            }

            /**
             * <summary>
             *   Convenience method to add <see cref="Symbol"/>s to
             *   <see cref="InternalExpression"/>. Equivalent to calling
             *   <c>Expression.InternalExpression.Add(Symbol)</c> on an instance of
             *   <see cref="Expression"/>.
             * </summary>
             */
            public void Add(Symbol s)
            {
                InternalExpression.Add(s);
            }

            /**
             * <summary>
             *   Conversion between <see cref="Expression"/> and <see cref="NumberSymbol"/>,
             *   used when unboxing singular <see cref="NumberSymbol"/>s from
             *   <see cref="InternalExpression"/>.
             * </summary>
             */
            public static explicit operator NumberSymbol(Expression expression)
            {
                return expression.InternalExpression.Single() is NumberSymbol ?
                    expression.InternalExpression.Single() as NumberSymbol : null;
            }
        }
    }
}
