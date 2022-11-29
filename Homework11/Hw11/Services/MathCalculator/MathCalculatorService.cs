using Hw11.Dto;
using Hw11.Services.ExpressionTree;
using Hw9.Services.Expressions;

namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        Validator.Validate(expression);
        var parseResult = Parser.ConvertToPostfixForm(expression);
        var exprTree = ExpressionTreeBuilder.CreateExpressionTree(parseResult);
        return await ExpressionTreeVisitor.VisitExpression((dynamic)exprTree);
    }
}