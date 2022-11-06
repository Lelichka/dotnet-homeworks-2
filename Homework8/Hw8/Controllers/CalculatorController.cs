using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Hw8.Calculator;
using Hw8.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hw8.Controllers;

public class CalculatorController : Controller
{
    public ActionResult<double> Calculate([FromServices] ICalculator calculator, [FromServices] IParser parser,
        string val1,
        string operation,
        string val2)
    {
        double value1, value2;
        if (!parser.ParseCalcArgument(val1, out value1) || !parser.ParseCalcArgument(val2, out value2))
            return BadRequest(Messages.InvalidNumberMessage);
        
        var calcOperation = parser.ParseCalcOperation(operation);
        
        if (calcOperation == CalculatorOperation.Undefined) return BadRequest(Messages.InvalidOperationMessage);
        
        if (value2 == 0) return BadRequest(Messages.DivisionByZeroMessage);
        return calculator.Calculate(value1, calcOperation, value2);
    }
    
    [ExcludeFromCodeCoverage]
    public IActionResult Index()
    {
        return Content(
            "Заполните val1, operation(plus, minus, multiply, divide) и val2 здесь '/calculator/calculate?val1= &operation= &val2= '\n" +
            "и добавьте её в адресную строку.");
    }
}