using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using Hw9.ErrorMessages;

namespace Hw9.Services.ExpressionTree;

public class ExpressionTreeVisitor : ExpressionVisitor
{
    protected override Expression VisitBinary(BinaryExpression node)
    {
        var res = CompileAsync(node.Left, node.Right).Result;
        return node.NodeType switch
        {
            ExpressionType.Add => Expression.Add(Expression.Constant(res[0]), Expression.Constant(res[1])),
            ExpressionType.Subtract => Expression.Subtract(Expression.Constant(res[0]), Expression.Constant(res[1])),
            ExpressionType.Multiply => Expression.Multiply(Expression.Constant(res[0]), Expression.Constant(res[1])),
            _ => res[1] != 0.0
                ? Expression.Divide(node.Left, node.Right)
                : throw new Exception(MathErrorMessager.DivisionByZero)
        };
    }

    public Task<Expression> CreateCalcExpr(Expression expr) =>
        Task.Run(() => base.Visit(expr));

    protected override Expression VisitConstant(ConstantExpression node)
    {
        return node;
    }

    private async Task<double[]> CompileAsync(Expression left, Expression right)
    {
        var leftExpr = Task.Run(async () => { Thread.Sleep(3000); return Expression.Lambda<Func<double>>(left).Compile().Invoke(); });
        var rightExpr = Task.Run(async () => { Thread.Sleep(3000); return Expression.Lambda<Func<double>>(right).Compile().Invoke(); });
        return await Task.WhenAll(leftExpr, rightExpr);
    }

}