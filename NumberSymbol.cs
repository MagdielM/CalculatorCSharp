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
        public double ToDouble() => numericValue;
    }
}
