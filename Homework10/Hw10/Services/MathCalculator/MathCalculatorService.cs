using Hw10.Dto;
using Hw10.Services.Expressions;
using Hw10.Services.ExpressionTree;

namespace Hw10.Services.MathCalculator;

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