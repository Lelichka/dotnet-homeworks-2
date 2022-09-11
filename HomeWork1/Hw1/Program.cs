using System.Xml;
using Hw1;

public class Program
{
    public static double CalculateResult { get; private set; }
    public static void Main(string[] args)
    {
        double arg1, arg2;
        CalculatorOperation operation;

        // TODO: implement calculator logic
        
        Parser.ParseCalcArguments(args,out arg1,out operation,out arg2);
        var result = Calculator.Calculate(arg1,operation,arg2);
        CalculateResult = result;
        Console.WriteLine(result);
    }
}
