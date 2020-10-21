namespace CalculatorCSharp
{
    class OperationSymbol : Symbol
    {
        public OperationType operationType;

        public OperationSymbol(OperationType operationType, string rawSymbol)
        {
            this.operationType = operationType;
            this.rawSymbol = rawSymbol;
        }
    }
}
