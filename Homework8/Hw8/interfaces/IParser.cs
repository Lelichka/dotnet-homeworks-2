using Hw8.Calculator;

namespace Hw8.Interfaces;

public interface IParser
{
    bool ParseCalcArgument(string arg, out double value);
    CalculatorOperation ParseCalcOperation(string arg);
}