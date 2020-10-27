namespace CalculatorCSharp
{
    public class NumberSymbol : Symbol
    {
        private double numericValue;

        public NumberSymbol(string numericString)
        {
            level = -1;
            NumericValue = double.Parse(numericString);
            rawSymbol = numericString;
        }

        public NumberSymbol(double value)
        {
            level = -1;
            NumericValue = value;
        }

        public double NumericValue
        {
            get { return numericValue; }
            set
            {
                numericValue = value;
                rawSymbol = value.ToString();
            }
        }

        public static implicit operator double(NumberSymbol symbol) => symbol?.numericValue ?? 0;
        public static implicit operator NumberSymbol(double number) => new NumberSymbol(number);
        /**
         * <summary>
         *   Returns a <see cref="NumberSymbol"/> with an equal negative value.
         * </summary>
         */
        public static NumberSymbol operator -(NumberSymbol item)
            => new NumberSymbol(-item.NumericValue);

        /**
         * <summary>
         *   Returns a <see cref="NumberSymbol"/> equal to the sum of the two
         *   <see cref="NumberSymbol"/>'s <see cref="NumericValue"/>s.
         * </summary>
         */
        public static NumberSymbol operator +(NumberSymbol left, NumberSymbol right)
            => new NumberSymbol(left.NumericValue + right.NumericValue);

        /**
         * <summary>
         *   Returns a <see cref="NumberSymbol"/> equal to the difference between the two
         *   <see cref="NumberSymbol"/>'s values.
         * </summary>
         */
        public static NumberSymbol operator -(NumberSymbol left, NumberSymbol right)
            => new NumberSymbol(left.NumericValue - right.NumericValue);

        /**
         * <summary>
         *   Returns a <see cref="NumberSymbol"/> equal to the product of the two
         *   <see cref="NumberSymbol"/>'s values.
         * </summary>
         */
        public static NumberSymbol operator *(NumberSymbol left, NumberSymbol right)
            => new NumberSymbol(left.NumericValue * right.NumericValue);

        /**
         * <summary>
         *   Returns a <see cref="NumberSymbol"/> equal to the quotient of the two
         *   <see cref="NumberSymbol"/>'s values.
         * </summary>
         */
        public static NumberSymbol operator /(NumberSymbol left, NumberSymbol right)
            => new NumberSymbol(left.NumericValue / right.NumericValue);

        /**
         * <summary>
         *   Returns a <see cref="NumberSymbol"/> equal to the value of this instance's
         *   <see cref="NumericValue"/> raised to the power of <paramref name="right"/>'s
         *   <see cref="NumericValue"/>.
         * </summary>
         * <param name="right">
         *   The <see cref="NumberSymbol"/> to raise this instance's value to.
         * </param>
         */
        public NumberSymbol Pow(NumberSymbol right)
            => new NumberSymbol(System.Math.Pow(numericValue, right));

        public static NumberSymbol Negate(NumberSymbol item)
        {
            return -item;
        }

        public static NumberSymbol Add(NumberSymbol left, NumberSymbol right)
        {
            return left + right;
        }

        public static NumberSymbol Subtract(NumberSymbol left, NumberSymbol right)
        {
            return left - right;
        }

        public static NumberSymbol Multiply(NumberSymbol left, NumberSymbol right)
        {
            return left * right;
        }

        public static NumberSymbol Divide(NumberSymbol left, NumberSymbol right)
        {
            return left / right;
        }

        public double ToDouble() => numericValue;
        public static NumberSymbol FromDouble(double number) => new NumberSymbol(number);
    }
}
