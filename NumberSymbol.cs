namespace CalculatorCSharp
{
    public class NumberSymbol : Symbol
    {

        public NumberSymbol(string numericString)
        {
            NumericValue = double.Parse(numericString);
            rawSymbol = numericString;
        }

        public double NumericValue { get; set; }
    }
}
