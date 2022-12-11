using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Services.Expressions;
using Hw9.Services.ExpressionTree;


namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            Validator.Validate(expression);
            var parseResult = Parser.ConvertToPostfixForm(expression);
            var exprTree = ExpressionTreeBuilder.CreateExpressionTree(parseResult);
            var result = 
                await ExpressionTreeVisitor.VisitAsync(exprTree);
            return new CalculationMathExpressionResultDto(result);
        }
        catch(Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
        
            
    }
}