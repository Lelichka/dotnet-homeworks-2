using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hw11.ErrorMessages;
using Hw11.Exceptions;

namespace Hw11.Services.ExpressionTree;

public class ExpressionTreeVisitor
{
    public static async Task<double> VisitExpression(Expression expr)
    {
        return await Visit((dynamic)expr);
    }
    async public static Task<double> Visit(BinaryExpression expression)
    {
        await Task.Delay(1000);
        var leftExpr = Task.Run(() => VisitExpression(expression.Left));
        var rightExpr = Task.Run(() => VisitExpression(expression.Right));
        var res = await Task.WhenAll(leftExpr, rightExpr);

        var constLeft = res[0];
        var constRight = res[1];

        return Calculate(expression.NodeType, constLeft, constRight);
            
    }
    async public static Task<double> Visit(ConstantExpression expression)
    {
        return (double)expression.Value;
    }

    public static double Calculate(ExpressionType binExpr, double constLeft,double constRight)
    {
        return (binExpr) switch
        {
            ExpressionType.Add => constLeft + constRight,
            ExpressionType.Subtract => constLeft - constRight,
            ExpressionType.Multiply => constLeft * constRight,
            ExpressionType.Divide => constRight == 0.0
                ? throw new DivideByZeroException(MathErrorMessager.DivisionByZero)
                : constLeft / constRight,
            _ => throw new InvalidSymbolException(MathErrorMessager.UnknownCharacter)
        };
    } 
}