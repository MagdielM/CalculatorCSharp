using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorCSharp
{
    class OperationSymbol : Symbol
    {
        public OperationType operationType;

        public OperationSymbol(char operatorCharacter)
        {
            switch (operatorCharacter)
            {
                case '+':
                    operationType = OperationType.Add;
                    break;

                case '-':
                    operationType = OperationType.Subtract;
                    break;

                case '×':
                case 'x':
                case '*':
                    operationType = OperationType.Multiply;
                    break;

                case '÷':
                case '/':
                    operationType = OperationType.Divide;
                    break;

                case '^':
                    operationType = OperationType.Power;
                    break;
                default:
                    throw new ArgumentException($"The operator \'{0}\' is invalid. " +
                        Calculator.defaultErrorMessageEnd);
            }
        }
    }
}
