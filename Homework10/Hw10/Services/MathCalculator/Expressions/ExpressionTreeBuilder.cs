using System.Linq.Expressions;

namespace Hw10.Services.Expressions;

public class ExpressionTreeBuilder
{
    public static Expression CreateExpressionTree(string input)
    {
        var stack = new Stack<Expression>();
        foreach (var elem in input.Split(" "))
        {
            if (elem == "" || elem == " ") continue;
            if (double.TryParse(elem, out var val))
                stack.Push(Expression.Constant(val));
            else
            {
                var right = stack.Pop();
                var left = stack.Pop();
                

                switch (elem)
                {
                    case "+" : stack.Push(Expression.Add(left, right)); break;
                    case "-" : stack.Push(Expression.Subtract(left, right)); break;
                    case "*" : stack.Push(Expression.Multiply(left, right)); break;
                    case "/" : stack.Push(Expression.Divide(left, right)); break;
                }
            }
        }
        return stack.Pop();
    }
}