
using Hw8.Interfaces;

namespace Hw8.Calculator;

public class Calculator : ICalculator
{
    public double Calculate(double val1, CalculatorOperation operation, double val2)
    {
        switch (operation)
        {
            case CalculatorOperation.Plus:
                return Plus(val1, val2);
            case CalculatorOperation.Minus:
                return Minus(val1, val2);
            case CalculatorOperation.Multiply:
                return Multiply(val1, val2);
            case CalculatorOperation.Divide:
                return Divide(val1, val2);
            
            default: throw new InvalidOperationException();
        }
    }
    public double Plus(double val1, double val2) => val1 + val2;

    public double Minus(double val1, double val2) => val1 - val2;

    public double Multiply(double val1, double val2) => val1 * val2;

    public double Divide(double firstValue, double secondValue)
    {
        if (secondValue == 0)
            throw new InvalidOperationException();
        return firstValue / secondValue;
    }
}