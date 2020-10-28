namespace CalculatorCSharp
{
    /**
     * <summary>
     *   Enum that determines the type of operation to perform.
     * </summary>
     * <remarks>
     *   The values are declared in reverse-precedence order to allow
     *   <see cref="Calculator.Expression.Resolve"/> to resolve the symbols in the correct order via
     *   simple iteration loops. Changing the order of these values could result in
     *   undesired behavior from <see cref="Calculator.Expression.Resolve"/>
     * </remarks>
     */
    public enum OperationType
    {
        AddOrSubtract, MultiplyOrDivide, Power, Invert
    }
}
