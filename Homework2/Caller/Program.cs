using Hw2;

public class Program
{
    public static double CalculateResult { get; private set; }
    public static void Main(string[] args)
    {
        double arg1, arg2;
        CalculatorOperation operation;
        Parser.ParseCalcArguments(args,out arg1,out operation,out arg2);
        if (operation == CalculatorOperation.Divide && arg2 == 0) throw new ArgumentException("Divide by zero");
        var result = Calculator.Calculate(arg1,operation,arg2);
        CalculateResult = result;
        Console.WriteLine(result);
    }
}