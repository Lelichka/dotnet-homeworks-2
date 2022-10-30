using System;
using System.Linq;
using Hw2;
using Xunit;

namespace Hw2Tests;

public class CalculatorMainTests
{
    [Theory]
    [InlineData("134", "+", "7",141)]
    [InlineData("32", "+", "16",48)]
    [InlineData("209", "-", "13",196)]
    [InlineData("59", "-", "10",49)]
    [InlineData("15", "*", "3",45)]
    [InlineData("13", "*", "4",52)]
    [InlineData("45", "/", "9",5)]
    [InlineData("81", "/", "9",9)]
    public void TestСorrectСalculations(string val1, string operation, string val2,double expectedResult)
    {
        var args = new[] { val1, operation, val2 };
        
        Program.Main(args);
        
        Assert.Equal(expectedResult,Program.CalculateResult);
    }
    
    [Theory]
    [InlineData("q", "+", "1")]
    [InlineData("1", "+", "q")]
    [InlineData("q", "+", "q")]
    [InlineData("2","/","0")]
    public void TestInvalidArguments(string val1, string operation, string val2)
    {
        var args = new[] { val1, operation};
        Assert.Throws<ArgumentException>(() => Program.Main(args));
        
        args = args.Append(val2).ToArray();
        Assert.Throws<ArgumentException>(() => Program.Main(args));
    }
    
    [Theory]
    [InlineData("1", ".", "1")]
    [InlineData("1", "7", "1")]
    [InlineData("1", "%", "1")]
    public void TestInvalidOperation(string val1, string operation, string val2)
    {
        var args = new[] { val1, operation, val2 };
        Assert.Throws<InvalidOperationException>(() => Program.Main(args));
    }
}