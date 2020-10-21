namespace CalculatorCSharp
{
    class OperationSymbol : Symbol
    {
        public OperationType operationType;

        public OperationSymbol(OperationType operationType, string rawSymbol)
        {
            level = (int)operationType;
            this.operationType = operationType;
            this.rawSymbol = rawSymbol;
        }
    }
}
