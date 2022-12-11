using System.Globalization;
using Hw8.Interfaces;

namespace Hw8.Calculator;

public class Parser: IParser
{
    public bool ParseCalcArgument(string arg, out double value)
    {
        return double.TryParse(arg, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out value);
    }
    
    public CalculatorOperation ParseCalcOperation(string arg)
    {
        return arg.ToLower() switch
        {
            "plus" => CalculatorOperation.Plus,
            "minus" => CalculatorOperation.Minus ,
            "multiply" => CalculatorOperation.Multiply,
            "divide" => CalculatorOperation.Divide,
            _ => CalculatorOperation.Undefined
        };
    }
}